using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.DapperInterfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using static Dapper.SqlMapper;

namespace Infrastructure.DapperRepository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string Connectionstring;
        private readonly IConfiguration _configuration;
        public DapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Connectionstring = _configuration.GetConnectionString("DefaultConnection");
        }
        private DbConnection GetDbconnection()
        {
            return new SqlConnection(Connectionstring);
        }
        public async Task<bool> ExecuteAsync(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var db = GetDbconnection())
            {
                return await db.ExecuteAsync(sp, parms, commandType: commandType) >= 0 ? true : false;
            }
        }
        public async Task<T> GetSingleAsync<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using var db = GetDbconnection();
                return await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<T>> GetAllAsync<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            { 
            using var db = GetDbconnection();
            var Data = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return Data.ToList();
            }catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<T>> ExecuteTransactionMultipleReturn<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = GetDbconnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var tran = db.BeginTransaction();
                try
                {
                    var result = await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                    return result.ToList();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
        }
        public async Task<T> ExecuteTransactionSingleReturn<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var db = GetDbconnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                var tran = db.BeginTransaction();
                try
                {
                    var result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                    return result;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                {
                    db.Close();
                }
            }
        }
        public async Task<List<object>> GetMultipleSelectsAsync(string sql, object? parameters = null, params Func<GridReader, object>[] readerFuncs)
        {
            /// <Usr>
            ///     === Example ===
            ///
            /// var foosAndBars = await GetMultipleSelectsAsync("SP_ReturnMultipleSelects", dTO,
            ///     x => x.ReadFirstOrDefaultAsync<CaseDetailsViewModel>(),
            ///     x => x.Read<GetCaseEmailsViewModel>().ToList());
            /// GetSingleCaseViewModel Result = new GetSingleCaseViewModel();
            /// Result.Case = (CaseDetailsViewModel)foosAndBars[0];
            /// Result.Emails = (List<GetCaseEmailsViewModel>)foosAndBars[1];
            ///
            /// </summary>
            var returnResults = new List<object>();
            using (IDbConnection db = GetDbconnection())
            {
                using var Result = await db.QueryMultipleAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                foreach (var readerFunc in readerFuncs)
                {
                    returnResults.Add(readerFunc(Result));
                }
            }
            return returnResults;
        }


        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }




    }
}

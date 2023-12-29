using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Domain.Interfaces.DapperInterfaces
{
    public interface IDapperRepository
    {
        Task<bool> ExecuteAsync(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<T> GetSingleAsync<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> GetAllAsync<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<List<T>> ExecuteTransactionMultipleReturn<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<T> ExecuteTransactionSingleReturn<T>(string sp, object? parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<List<object>> GetMultipleSelectsAsync(string sql, object? parameters = null, params Func<GridReader, object>[] readerFuncs);
        DataTable ToDataTable<T>(List<T> items);
    }
}

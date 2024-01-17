using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.EfInterfaces
{
    public interface IEntityFrameWork
    {
        Task<List<T1>> GetAllAsync<T1,T2>(/*int pageNumber = 1, int pageSize = 10*/) where T2 : class;
        Task<List<T1>> GetRoleAsync<T1>() where  T1: class;
        Task<T1> CreateAsync<T1, T2>(T1 obj) where T2 : class;
        Task<T1> UpdateAsync<T1, T2>(T1 obj) where T2 : class;
        //public async Task<bool> DeleteAsync<T1>(Expression<Func<T1, bool>> predicate) where T1 : class

       Task<bool> DeleteAsync<T1>(Expression<Func<T1, bool>> predicate) where T1: class;
        Task<IQueryable<T2>> GetWithInclude<T2>(Expression<Func<T2, bool>> predicate, params Expression<Func<T2, object>>[] includes) where T2 : class;
    }
}

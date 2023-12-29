using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    }
}

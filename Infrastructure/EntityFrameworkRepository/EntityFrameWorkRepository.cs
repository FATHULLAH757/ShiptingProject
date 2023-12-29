using AutoMapper;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.EfInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.EntityFrameworkRepository
{
    public class EntityFrameWorkRepository : IEntityFrameWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private RoleManager<IdentityRole<int>> _roleManager;
        
        public EntityFrameWorkRepository(ApplicationDbContext context, IMapper mapper, RoleManager<IdentityRole<int>> identityRole)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = identityRole;
        }

        public async Task<List<T1>> GetAllAsync<T1, T2>(/*int pageNumber = 1, int pageSize = 10*/) where T2 : class
        {
            var table = _context.Set<T2>();
            //pageNumber = (pageNumber <= 0) ? 1 : pageNumber;
            //pageSize = (pageSize <= 0) ? 10 : pageSize;


            //var totalRecords = table.Count();
            //var totalPages = Math.Ceiling((double)totalRecords / pageSize);


          //  var skip = (pageNumber - 1) * pageSize;


            //var users = new List<AllUsersViewModel>();

            //table.ToList();
            //var users1 = table.Skip(skip).Take(pageSize).ToList();
            var tblList = table.ToList();

            var list = _mapper.Map<List<T2>, List<T1>>(tblList);
            return (list);
        }

        public async Task<List<T1>> GetRoleAsync<T1>() where T1 : class
        {
            var roleList = _roleManager.Roles.ToList();  
            var list = _mapper.Map<List<T1>>(roleList);
            return (list);
        }
        public async Task<T1> CreateAsync<T1, T2>(T1 obj) where T2 : class
        {
            try
            {
                var table = _context.Set<T2>();
                var data = _mapper.Map<T1, T2>(obj);
                var result = table.AddAsync(data).Result;
                _context.SaveChanges();
                PropertyInfo propertyInfo = typeof(T1).GetProperty("Id");
                propertyInfo.SetValue(obj, result.Property("Id").OriginalValue);
                return (obj);
            }
            catch (Exception ex)
            {
                PropertyInfo propertyInfo = typeof(T1).GetProperty("Id");
                propertyInfo.SetValue(obj, 0);
                return (obj);
            }
        }
        public async Task<T1> UpdateAsync<T1, T2>(T1 obj) where T2 : class
        {
            try
            {
                var table = _context.Set<T2>();
                var data = _mapper.Map<T1, T2>(obj);
                var result = table.Update(data);
                _context.SaveChanges();
                return (obj);
            }
            catch (Exception ex)
            {
                PropertyInfo propertyInfo = typeof(T1).GetProperty("Id");
                propertyInfo.SetValue(obj, 0);
                return (obj);
            }
        }
    }
}

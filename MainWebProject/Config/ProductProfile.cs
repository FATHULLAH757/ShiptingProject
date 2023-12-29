using AutoMapper;
using Domain.Entities;
using MainWebProject.Models;
using Microsoft.AspNetCore.Identity;

namespace MainWebProject.Config
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<IdentityRole<int>, UserRoleVM>();
            CreateMap<DriverVM, Driver>();
            CreateMap<Driver, DriverVM>();
        }

    }
}

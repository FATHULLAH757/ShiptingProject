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
            CreateMap<WorkOrderVM, WorkOrder>();
            CreateMap<WorkOrder, WorkOrderVM>().ForMember(x => x.DestinationBusinessDetail, y => y.Ignore()).ForMember(x => x.PickupBusinessDetail, y => y.Ignore());
        }

    }
}

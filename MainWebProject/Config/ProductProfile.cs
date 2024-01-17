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
            CreateMap<DriverVM, Driver>().ReverseMap();
            //CreateMap<Driver, DriverVM>();
            CreateMap<WorkOrderVM, WorkOrder>().ReverseMap();
            CreateMap<WorkOrderDestinationVm, WorkOrderDestination>().ReverseMap();
            CreateMap<WorkOrderPickupVm, WorkOrderPickup>().ReverseMap();
            CreateMap<AdditionalChargesVM, WorkOrderAdditionalCharges>().ReverseMap();
            CreateMap<WorkOrderDrayageVM, Drayage>().ReverseMap();
            //CreateMap<WorkOrder, WorkOrderVM>().ForMember(x => x.DestinationBusinessDetail, y => y.Ignore()).ForMember(x => x.PickupBusinessDetail, y => y.Ignore());
        }

    }
}

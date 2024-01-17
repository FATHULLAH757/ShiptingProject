using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.DapperInterfaces;
using Domain.Interfaces.EfInterfaces;
using Infrastructure.DapperRepository;
using MainWebProject.Config;
using MainWebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Linq;

namespace MainWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEntityFrameWork _Ef;
        private readonly IDapperRepository _Dp;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IEntityFrameWork Ef,IDapperRepository Dp, UserManager<User> userManager, IMapper mapper)
        {
            _logger = logger;
            _Ef = Ef;
            _Dp = Dp;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllOrder()
        {
          
           // var list = _Ef.GetAllAsync<WorkOrderVM, WorkOrder>().Result;
            var list1 = _Dp.GetAllAsync<WorkOrderVM>("sp_GetAllWorkOrder").Result;
            return View(list1);
        }
        public IActionResult WorkOrder(int Id = 0)
        {
            var list = _Ef.GetAllAsync<DriverVM, Driver>().Result;
           
            //List<DriverVM> DriverList = new List<DriverVM>();
            //foreach (var item in list)
            //{
            //    DriverList.Add(new DriverVM { Id = item.Id, Name = item.Name });
            //}
            
            ViewBag.DriverList = list.Select(x => new SelectListItem {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            List<DriverVM> TruckList = new List<DriverVM>();
            TruckList.Add(new DriverVM { Id = 1, Name = "Truck" });
            ViewBag.TruckList = TruckList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            var result = _mapper.Map< WorkOrderVM>(_Ef.GetWithInclude<WorkOrder>(a => a.Id == Id, x => x.PickupBusinessDetail, p => p.DestinationBusinessDetail, p => p.WorkOrderAdditionalCharges, e => e.WorkOrderDrayage).Result.FirstOrDefault());
           // var resultToVM = _mapper<WorkOrderVM>(result);
            return View(result);
        }
        [HttpPost]
        public  async Task<IActionResult> WorkOrder(WorkOrderVM workOrderVM)
        {


            try
            {
                var file = Request.Form.Files.Count() > 0 ? Request.Form.Files[0] : null;
                WorkOrderVM result = new WorkOrderVM();
                var result1 = _mapper.Map<WorkOrder>(workOrderVM);
                string message = null;
                if (workOrderVM.Id > 0)
                {
                    var newcategoriesUnit =  _Ef.GetWithInclude<WorkOrder>(a => a.Id == workOrderVM.Id, x => x.PickupBusinessDetail, p => p.DestinationBusinessDetail).Result.FirstOrDefault();
                    var deletedPickupOrder = newcategoriesUnit?.PickupBusinessDetail.Select(x => x.Id).Except(result1.PickupBusinessDetail.Select(x => x.Id)).ToList();
                    var deletedDestinationOrder = newcategoriesUnit?.DestinationBusinessDetail.Select(x => x.Id).Except(result1.DestinationBusinessDetail.Select(x => x.Id)).ToList();
                    if (deletedPickupOrder != null)
                    {
                        foreach (int id in deletedPickupOrder)
                        {
                            var isRecordDeleted = _Ef.DeleteAsync<WorkOrderPickup>(x => x.Id == id);
                            foreach (var n in result1.PickupBusinessDetail.Where(x => x.Id == id).ToArray()) result1.PickupBusinessDetail.Remove(n);
                        }
                    }
                    if (deletedDestinationOrder != null)
                    {
                        foreach (int id in deletedDestinationOrder)
                        {
                            var isRecordDeleted = _Ef.DeleteAsync<WorkOrderDestination>(x => x.Id == id);
                            foreach (var n in result1.DestinationBusinessDetail.Where(x => x.Id == id).ToArray()) result1.DestinationBusinessDetail.Remove(n);
                        }
                    }
                    var updatePickup = result1.PickupBusinessDetail.Where(x => x.Id != 0).ToList();
                    var updateDestination = result1.DestinationBusinessDetail.Where(x => x.Id != 0).ToList();

                    //if (updatePickup != null) {
                    //    for ( int i = 0; i< updatePickup.Count(); i++)
                    //    {
                    //        updatePickup[i].WorkOrderId = result1.Id;
                    //        var isRecordUpdate = _Ef.UpdateAsync<WorkOrderPickupVm, WorkOrderPickup>(_mapper.Map<WorkOrderPickupVm>(updatePickup[i]));
                    //        result1.PickupBusinessDetail.Remove(updatePickup[i]);
                    //        //foreach (var n in result1.PickupBusinessDetail.Where(x => x.Id == item.Id).ToArray()) result1.PickupBusinessDetail.Remove(n);
                    //    }
                    //}
                    //if (updateDestination != null)
                    //{
                    //    for (int i = 0; i < updateDestination.Count(); i++)
                    //    {
                    //        updateDestination[i].WorkOrderId = result1.Id;
                    //        var isRecordUpdate = _Ef.UpdateAsync<WorkOrderDestinationVm, WorkOrderDestination>(_mapper.Map<WorkOrderDestinationVm>(updateDestination[i]));
                    //        result1.DestinationBusinessDetail.Remove(updateDestination[i]);
                    //        //foreach (var n in result1.PickupBusinessDetail.Where(x => x.Id == item.Id).ToArray()) result1.PickupBusinessDetail.Remove(n);
                    //    }
                    //    //foreach (var item in updateDestination)
                    //    //{
                    //    //    item.WorkOrderId = result1.Id;
                    //    //    var isRecordUpdate = _Ef.UpdateAsync<WorkOrderDestinationVm, WorkOrderDestination>(_mapper.Map<WorkOrderDestinationVm>(item));
                    //    //    result1.DestinationBusinessDetail.Remove(item);
                    //    //   // foreach (var n in result1.PickupBusinessDetail.Where(x => x.Id == item.Id).ToArray()) result1.PickupBusinessDetail.Remove(n);
                    //    //}
                    //}



                    result = _Ef.UpdateAsync<WorkOrderVM, WorkOrder>(_mapper.Map<WorkOrderVM>(result1)).Result;
                    message = "Work Order Updated Successfully";
                }
                else
                {
                    //workOrderVM.WorkOrderAdditionalCharges = null;
                    result = _Ef.CreateAsync<WorkOrderVM, WorkOrder>(workOrderVM).Result;
                    message = "Work Order Created Successfully";
                }

                if (result.Id > 0)
                {
                    return Ok(new { success = true, message = message });
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }



            return Ok(new { success = true});
        }
        
        public async Task<JsonResult> DeleteWorkOrder(int orderId)
        {
            try
            {
                //Message response = new Message();
               var  response = await _Ef.DeleteAsync<WorkOrder>(x => x.Id == orderId);

                if (response)
                {
                    return new JsonResult(new { Success = true, Message = "Order Deleted Successfully", StatusCode = 200 });
                }
                return new JsonResult(new { Success = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false });
            }
        }
        public IActionResult AllDriver()
        {
            var list = _Ef.GetAllAsync<DriverVM, Driver>().Result;

            return View(list);
        }
        [HttpGet]
        public IActionResult CreateDriver(int Id = 0)
        {
            var role = _Ef.GetRoleAsync<UserRoleVM>().Result;
            ViewBag.DriverList = role.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var result = _mapper.Map<DriverVM>(_Ef.GetWithInclude<Driver>(a => a.Id == Id).Result.FirstOrDefault());
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDriver(DriverVM driverVM)
        {
            try
            {
                var file = Request.Form.Files.Count()> 0 ? Request.Form.Files[0]: null;
                DriverVM result = new DriverVM();
                string message = null;
                if (driverVM.Id > 0) 
                {
                    result = _Ef.UpdateAsync<DriverVM, Driver>(driverVM).Result;
                    message = "Driver Updated Successfully";
                }
                else
                {
                    //var adminUser = await _userManager.FindByEmailAsync("");
                    //if (adminUser == null)
                    //{
                    //    var newAdminUser = new User()
                    //    {
                    //        FullName = "Admin User",
                    //        UserName = "admin-user",
                    //        Email = "",
                    //        EmailConfirmed = true,
                    //        Description = "abc"
                    //    };
                    //    await _userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    //    await _userManager.AddToRoleAsync(newAdminUser, UserRoles.Driver);
                    //}
                    result = _Ef.CreateAsync<DriverVM, Driver>(driverVM).Result;
                    message = "Driver Created Successfully";
                }
                
                if (result.Id > 0)
                {
                    return Ok(new { success = true, message = message });
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        public async Task<JsonResult> DeleteDriver(int driverId)
        {
            try
            {
                //Message response = new Message();
                var response = await _Ef.DeleteAsync<Driver>(x => x.Id == driverId);

                if (response)
                {
                    return new JsonResult(new { Success = true, Message = "Driver Deleted Successfully", StatusCode = 200 });
                }
                return new JsonResult(new { Success = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false });
            }
        }
        [HttpGet]
        public IActionResult Truck()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Truck(TruckVM truckVM)
        {

            return Ok(new { success = true });
        }
        public IActionResult BoxIcon()
        {
            return View();
        }
        public IActionResult EventBoard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

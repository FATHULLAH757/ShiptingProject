using Domain.Entities;
using Domain.Interfaces.DapperInterfaces;
using Domain.Interfaces.EfInterfaces;
using MainWebProject.Config;
using MainWebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace MainWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEntityFrameWork _Ef;
        private readonly IDapperRepository _Dp;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IEntityFrameWork Ef,IDapperRepository Dp, UserManager<User> userManager)
        {
            _logger = logger;
            _Ef = Ef;
            _Dp = Dp;
            _userManager = userManager; 
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllOrder()
        {

            //var list = _Ef.GetAllAsync<WorkOrderVM, WorkOrder>().Result;
            var list1 = _Dp.GetAllAsync<WorkOrderVM>("sp_GetAllWorkOrder").Result;
            return View(list1);
        }
        public IActionResult WorkOrder()
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

            return View();
        }
        [HttpPost]
        public IActionResult WorkOrder(WorkOrderVM workOrderVM)
        {


            try
            {
                var file = Request.Form.Files.Count() > 0 ? Request.Form.Files[0] : null;
                WorkOrderVM result = new WorkOrderVM();
                string message = null;
                if (workOrderVM.Id > 0)
                {
                    result = _Ef.UpdateAsync<WorkOrderVM, WorkOrder>(workOrderVM).Result;
                    message = "Work Order Updated Successfully";
                }
                else
                {
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
        public IActionResult AllDriver()
        {
            var list = _Ef.GetAllAsync<DriverVM, Driver>().Result;

            return View(list);
        }
        [HttpGet]
        public IActionResult CreateDriver()
        {
            var role = _Ef.GetRoleAsync<UserRoleVM>().Result;
            ViewBag.DriverList = role.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View();
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

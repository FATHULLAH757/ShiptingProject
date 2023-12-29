using Domain.Entities;
using Domain.Interfaces.EfInterfaces;
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

        public HomeController(ILogger<HomeController> logger, IEntityFrameWork Ef)
        {
            _logger = logger;
            _Ef = Ef;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllOrder()
        {
           

            return View();
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
        public IActionResult CreateDriver(DriverVM driverVM)
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

using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.DapperInterfaces;
using Domain.Interfaces.EfInterfaces;
using Infrastructure;
using MainProject.Config;
using MainProject.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MainProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IEntityFrameWork _entityFrameWork;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(IDapperRepository dapperRepository, ApplicationDbContext context, IEntityFrameWork entityFrameWork, IMapper mapper)
        {
            _dapperRepository = dapperRepository;
            _context = context;
            _entityFrameWork = entityFrameWork;
            _mapper = mapper;
        }
        [HttpGet]
        public string Index()
        {

            return "Running Successfully";
        }

        [HttpGet]
        [Route("test1")]
        public IActionResult TestMethod()
        {
            
         //   var URl = CompleteURLs.testMethod;
            
          
            var prodList = _entityFrameWork.GetAllAsync<ShopDto, Shop>(1, 11).Result;
            return Ok(prodList);
        }
        [HttpGet]
        [Route("getallproduct")]
        public async Task<IActionResult> GetAllProduct()
        {

            var prodList = _entityFrameWork.GetAllAsync<ShopDto, Shop>(1, 11).Result;
            return Ok(prodList);
        }
    }
}

using KnowbaseService.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.Controllers
{
    [Route("/Knowbase/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly IKnowbaseRepo _repository;
        private readonly AppDbContext _context;
        public CountController(
           AppDbContext context,
          IKnowbaseRepo repository)
        {
            _repository = repository;
            _context = context;

        }

        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<int> GetCount()
        {



            var knowbaseItem = _repository.GetAllKnowbases();


            return Ok(knowbaseItem.ToArray().Length);


        }
    }
}

using CommunityService.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Controllers
{
    [Route("/post/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly IPostRepo _repository;
        private readonly AppDbContext _context;
        public CountController(
           AppDbContext context,
          IPostRepo repository)
        {
            _repository = repository;
            _context = context;

        }

        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<int> GetCount()
        {

            return Ok(_context.Posts.ToArray().Length);

        }
    }
}

using AutoMapper;
using CommunityService.Data;
using CommunityService.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoniterController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPostRepo _repository;
        private readonly IMapper _mapper;


        public MoniterController(IPostRepo repository, IMapper mapper, AppDbContext context)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;

        }


        [HttpPost]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<Boolean> DeletePost(MoniterPostReadDto moniterPostReadDto)
        {
            var post = _context.Posts.Find(moniterPostReadDto.Id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            if (_repository.SaveChanges())
            {
                return Ok(true);
            }
            return NotFound(false);
        }
    }
}

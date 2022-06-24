using AutoMapper;
using CommunityService.Data;
using CommunityService.Dtos;
using CommunityService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPostRepo _repository;
        private readonly IMapper _mapper;


        public PostController(IPostRepo repository, IMapper mapper, AppDbContext context)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;

        }


        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            Console.WriteLine("--> Getting Platforms....");

            var postsItem = _repository.GetAllPosts();

            Console.WriteLine("--> Getting Platforms.... Done");

            return Ok(postsItem);
        }


        [HttpGet("{id}", Name = "GetPostById")]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<Post> GetPostById(int id)
        {
            var postItem = _repository.GetPostById(id);
            if (postItem != null)
            {
                return Ok(postItem);
            }

            return NotFound();
        }


        [HttpPost]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<Post> CreatePost(PostCreateDto postCreateDto)
        {
            var postModel = _mapper.Map<Post>(postCreateDto);
            _repository.CreatePost(postModel);
            _repository.SaveChanges();


            var postReadDto = _mapper.Map<PostReadDto>(postModel);


            return CreatedAtRoute(nameof(GetPostById), new { Id = postReadDto.Id }, postReadDto);
        }



    }

}

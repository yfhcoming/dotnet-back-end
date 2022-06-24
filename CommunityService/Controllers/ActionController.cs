using AutoMapper;
using CommunityService.Data;
using CommunityService.Dtos;
using CommunityService.Models;
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
    public class ActionController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IPostRepo _repository;
        private readonly IMapper _mapper;


        public ActionController(IPostRepo repository, IMapper mapper, AppDbContext context)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;

        }

        //1 给帖子点赞
        [HttpPost]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<Boolean> LikePost(LikePostReadDto likePostReadDto)
        {
            Console.WriteLine("--> Hit LikePost");
/*            Console.WriteLine(likePostDto.Id);*/
            var likeNum = _context.Posts.Find(likePostReadDto.Id).LikeNum;
/*            Console.WriteLine(likeNum);*/
            _context.Posts.Find(likePostReadDto.Id).LikeNum = likeNum + 1;
            return _repository.SaveChanges();
        }

        //2 按帖子点赞数排序
        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<List<Post>> GetAllPostsByLike()
        { 

            Console.WriteLine("--> Hit GetAllPostsByLike");

            var postsItem = _repository.GetAllPostsByLike();

            return Ok(postsItem);
        }


        // 搜索帖子
        [Route("{keyword}", Name = "SearchPost")]
        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<IEnumerable<Post>> SearchPost(string keyword)
        {
            var postList = _context.Posts.Where(p => p.Title.Contains(keyword)
            || p.Content.Contains(keyword)).ToList();
            return Ok(postList);
        }
    }
}

using AutoMapper;
using CommunityService.Dtos;
using CommunityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Data
{
    public class PostRepo : IPostRepo
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PostRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public void CreatePost(Post Post)
        {
            if (Post == null)
            {
                throw new ArgumentNullException(nameof(Post));
            }

            _context.Posts.Add(Post);
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public List<LikePostDto> GetAllPostsByLike()
        {
            Console.WriteLine("--> Hit Repo");

            var posts = _context.Posts.ToList();

            List<LikePostDto> lists = new List<LikePostDto>();
            foreach (var post in posts)
            {
                lists.Add(_mapper.Map<LikePostDto>(post));
            }

            lists.Sort();
            return lists;
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

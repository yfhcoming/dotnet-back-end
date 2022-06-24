using CommunityService.Dtos;
using CommunityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Data
{
    public interface IPostRepo
    {

        bool SaveChanges();

        IEnumerable<Post> GetAllPosts();

        Post GetPostById(int id);

        void CreatePost(Post Post);

        /*        void RemovePost(Post post);*/

        /*        void LikePost(int id);*/


        List<LikePostDto> GetAllPostsByLike();

    }
}

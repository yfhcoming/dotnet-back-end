using AutoMapper;
using CommunityService.Dtos;
using CommunityService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityService.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreateDto, Post>();

            CreateMap<Post, PostReadDto>();

            CreateMap<Post, LikePostDto>();

        }


    }
}

using AutoMapper;
using KnowbaseService.Dtos;
using KnowbaseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.Profils
{
    public class KnowbaseProfile : Profile
    {

        public KnowbaseProfile()
        {
            // source -> Target

            CreateMap<Knowbase, KnowbaseReadDto>();

            CreateMap<KnowbaseCreateDto, Knowbase>();

            CreateMap<KnowbaseReadDto, KnowbasePublishedDto>();

            CreateMap<Knowbase, GrpcKnowbaseModel>()
            .ForMember(dest => dest.KnowbaseId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Img, opt => opt.MapFrom(src => src.img));
        }
    }
}

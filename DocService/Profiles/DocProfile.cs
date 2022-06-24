using AutoMapper;
using DocService.Dtos;
using DocService.Models;
using KnowbaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Profiles
{
    public class DocProfile : Profile
    {
        public DocProfile()
        {
            // Source -> Target
            CreateMap<Knowbase, KnowbaseReadDto>();

            CreateMap<DocCreateDto, Doc>();

            CreateMap<Doc, DocReadDto>();

            CreateMap<KnowbasePublishedDto, Knowbase>()
                     .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id))
                     .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<GrpcKnowbaseModel, Knowbase>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.KnowbaseId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.img, opt => opt.MapFrom(src => src.Img))
                .ForMember(dest => dest.Docs, opt => opt.Ignore());

        }

    }
}

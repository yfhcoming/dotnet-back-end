using AutoMapper;
using DocService.Data;
using DocService.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Controllers
{
    [Route("/docks")]
    [ApiController]
    public class KnowbaseController : ControllerBase
    {

        private readonly IDocRepo _repository;
        private readonly IMapper _mapper;

        public KnowbaseController(IDocRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<IEnumerable<KnowbaseReadDto>> Getknowbases()
        {
            Console.WriteLine("--> Getting Platforms from CommandsService");

            var knowbaseItems = _repository.GetAllKnowbases();

            return Ok(_mapper.Map<IEnumerable<KnowbaseReadDto>>(knowbaseItems));
        }

        [HttpGet("{id}", Name = "GetKnowbaseById")]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<KnowbaseReadDto> GetKnowbaseById(int id)
        {
            var knowbaseItem = _repository.GetKnowbaseById(id);
            if (knowbaseItem != null)
            {
                return Ok(_mapper.Map<KnowbaseReadDto>(knowbaseItem));
            }

            return NotFound();
        }

        [HttpPost]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controler");
        }

    }
}


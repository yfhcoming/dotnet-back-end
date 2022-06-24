using AutoMapper;
using DocService.Data;
using DocService.Dtos;
using DocService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Controllers
{
    [Route("d/knowbase/{KnowbaseId}/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {
        private readonly IDocRepo _repository;
        private readonly IMapper _mapper;

        public DocController(IDocRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<IEnumerable<DocReadDto>> GetDocsForKnowbase(int KnowbaseId)
        {
            Console.WriteLine($"--> Hit GetDocsForKnowbase: {KnowbaseId}");

            if (!_repository.KnowbaseExits(KnowbaseId))
            {
                return NotFound();
            }

            var docs = _repository.GetDocsForKnowbase(KnowbaseId);

            return Ok(_mapper.Map<IEnumerable<DocReadDto>>(docs));
        }

        [HttpGet("{docId}", Name = "GetDocForKnowbase")]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<DocReadDto> GetDocForKnowbase(int KnowbaseId, int docId)
        {
            Console.WriteLine($"--> Hit GetDocForKnowbase: {KnowbaseId} / {docId}");

            if (!_repository.KnowbaseExits(KnowbaseId))
            {
                return NotFound();
            }

            var doc = _repository.GetDoc(KnowbaseId, docId);

            if (doc == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocReadDto>(doc));
        }

        [HttpPost]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<DocReadDto> CreateDocForKnowbase(int KnowbaseId, DocCreateDto docCreateDto)
        {
            Console.WriteLine($"--> Hit CreateDocForKnowbase: {KnowbaseId}");

            if (!_repository.KnowbaseExits(KnowbaseId))
            {
                return NotFound();
            }

            var doc = _mapper.Map<Doc>(docCreateDto);

            _repository.CreateDoc(KnowbaseId, doc);
            _repository.SaveChanges();

            var docReadDto = _mapper.Map<DocReadDto>(doc);

            return CreatedAtRoute(nameof(GetDocForKnowbase),
                new { KnowbaseId = KnowbaseId, docId = docReadDto.Id }, docReadDto);
        }


    }
}

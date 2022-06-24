using AutoMapper;
using KnowbaseService.AsyncDataServices;
using KnowbaseService.Data;
using KnowbaseService.Dtos;
using KnowbaseService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using ServiceCount;

namespace KnowbaseService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KnowbaseController : ControllerBase 
    {

        private readonly IKnowbaseRepo _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        private int num = 0;

        [DllImport("LogWriter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern char printLog();


        public KnowbaseController(
            IKnowbaseRepo repository,
            IMapper mapper,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;

            _messageBusClient = messageBusClient;


        }


        [HttpGet]
        [EnableCors("CorsPolicy")] //允许跨域
        public ActionResult<IEnumerable<KnowbaseReadDto>> GetKnowbases()
        {

            
            Console.WriteLine(printLog());

            var knowbaseItem = _repository.GetAllKnowbases();

            var numChange = CountClass.addOne(num);
            Console.WriteLine(numChange);
            this.num++;


            return Ok(_mapper.Map<IEnumerable<KnowbaseReadDto>>(knowbaseItem));
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
        public ActionResult<KnowbaseReadDto> CreateKnowbase(KnowbaseCreateDto knowbaseCreateDto)
        {
            Console.WriteLine($"--> Hit CreateKnowbase");
            var knowbaseModel = _mapper.Map<Knowbase>(knowbaseCreateDto);
            _repository.CreateKnowbase(knowbaseModel);
            _repository.SaveChanges();

            knowbaseModel.img = "https://xingqiu-tuchuang-1256524210.cos.ap-shanghai.myqcloud.com/3227/image-20220624112354478.png";
            _repository.SaveChanges();

            var knowbaseReadDto = _mapper.Map<KnowbaseReadDto>(knowbaseModel);

            /*// Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }*/

            //Send Async Message
            try
            {
                var knowbasePublishedDto = _mapper.Map<KnowbasePublishedDto>(knowbaseReadDto);
                knowbasePublishedDto.Event = "Knowbase_Published";
                _messageBusClient.PublishNewKnowbase(knowbasePublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetKnowbaseById), new { Id = knowbaseReadDto.Id }, knowbaseReadDto);
        }
    }
}

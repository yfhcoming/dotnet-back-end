using AutoMapper;
using DocService.Models;
using Grpc.Net.Client;
using KnowbaseService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.SyncDataServices.Grpc
{
    public class KnowbaseDataClient : IKnowbaseDataClient
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public KnowbaseDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Knowbase> ReturnAllKnowbases()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcKnowbase"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcKnowbase"]);
            var client = new GrpcKnowbase.GrpcKnowbaseClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllKnowbases(request);
                return _mapper.Map<IEnumerable<Knowbase>>(reply.Knowbase);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}

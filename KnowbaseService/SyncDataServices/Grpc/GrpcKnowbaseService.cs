using AutoMapper;
using Grpc.Core;
using KnowbaseService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.SyncDataServices.Grpc
{
    public class GrpcKnowbaseService: GrpcKnowbase.GrpcKnowbaseBase
    {
        private readonly IKnowbaseRepo _repository;
        private readonly IMapper _mapper;

        public GrpcKnowbaseService(IKnowbaseRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<KnowbaseResponse> GetAllKnowbases(GetAllRequest request, ServerCallContext context)
        {
            var response = new KnowbaseResponse();
            var knowbases = _repository.GetAllKnowbases();

            foreach (var kbas in knowbases)
            {
                response.Knowbase.Add(_mapper.Map<GrpcKnowbaseModel>(kbas));
            }

            return Task.FromResult(response);
        }
    }
}

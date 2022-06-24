using DocService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.SyncDataServices.Grpc
{
    public interface IKnowbaseDataClient
    {
        IEnumerable<Knowbase> ReturnAllKnowbases();

    }
}

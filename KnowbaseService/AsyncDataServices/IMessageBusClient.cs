using KnowbaseService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewKnowbase(KnowbasePublishedDto knowbasePublishedDto);
    }
}

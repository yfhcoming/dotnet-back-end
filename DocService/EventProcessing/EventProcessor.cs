using AutoMapper;
using DocService.Data;
using DocService.Dtos;
using DocService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.KnowbasePublished:
                    addKnowbase(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Knowbase_Published":
                    Console.WriteLine("--> Knowbase Published Event Detected");
                    return EventType.KnowbasePublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addKnowbase(string knowbasePublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IDocRepo>();

                var knowbasePublishedDto = JsonSerializer.Deserialize<KnowbasePublishedDto>(knowbasePublishedMessage);

                try
                {
                    var knowbase = _mapper.Map<Knowbase>(knowbasePublishedDto);
                    if (!repo.ExternalKnowbaseExists(knowbase.ExternalID))
                    {
                        repo.CreateKnowbase(knowbase);
                        repo.SaveChanges();
                        Console.WriteLine("--> Knowbase added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Knowbase already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Knowbase to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        KnowbasePublished,
        Undetermined
    }
}

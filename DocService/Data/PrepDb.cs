using DocService.Models;
using DocService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Data
{
    public class PrepDb
    {

        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IKnowbaseDataClient>();

                var knowbases = grpcClient.ReturnAllKnowbases();

                SeedData(serviceScope.ServiceProvider.GetService<IDocRepo>(), knowbases);
            }
        }

        private static void SeedData(IDocRepo repo, IEnumerable<Knowbase> knowbases)
        {
            Console.WriteLine("Seeding new knowbases...");

            foreach (var kbas in knowbases)
            {
                if (!repo.ExternalKnowbaseExists(kbas.ExternalID))
                {
                    repo.CreateKnowbase(kbas);
                }
                repo.SaveChanges();
            }
        }
    }
}

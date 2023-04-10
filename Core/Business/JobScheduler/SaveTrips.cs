using System;
using BlitzerCore.Utilities;
using Quartz;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace BlitzerCore.Business.JobScheduler
{


    [DisallowConcurrentExecution]
    public class SaveTrips : IJob
    {
        private IServiceProvider ServiceProvider { get; set; }

        public SaveTrips(IServiceProvider aDbContext)
        {
            ServiceProvider = aDbContext;
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var lDbContext = scope.ServiceProvider.GetService<IDbContext>();
                var lTripBiz = new TripBusiness(lDbContext);
                var lTrips = lTripBiz.GetActiveTrips();
                foreach (var lTrip in lTrips)
                    lTripBiz.Save(lTrip);
                return Task.CompletedTask;

            }
        }
    }
}
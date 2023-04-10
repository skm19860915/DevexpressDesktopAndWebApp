using System;
using BlitzerCore.Utilities;
using Quartz;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace BlitzerCore.Business.JobScheduler {

    [DisallowConcurrentExecution]
    public class EsclateDeadline : IJob
    {
        private IServiceProvider ServiceProvider { get; set; }

        public EsclateDeadline(IServiceProvider aDbContext)
        {
            ServiceProvider = aDbContext;
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var lDbContext = scope.ServiceProvider.GetService<IDbContext>();
                new TaskBusiness(lDbContext).EscalteDeadLineTasks(DateTime.Now);
                return Task.CompletedTask;

            }
        }
    }
}
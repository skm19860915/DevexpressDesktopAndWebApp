using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace BlitzerCore.Business.JobScheduler
{
    public class JobFactory : IJobFactory
    {
        private IServiceProvider ServiceProvider { get;}

        public JobFactory(IServiceProvider aService)
        {
            ServiceProvider = aService;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return ActivatorUtilities.CreateInstance(ServiceProvider, bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            if ( job is IDisposable Disposable)
                Disposable.Dispose();
        }
    }
}

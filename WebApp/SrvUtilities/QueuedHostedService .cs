using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlitzerCore.Utilities;
using Microsoft.Extensions.Hosting;

namespace WebApp.SrvUtilities
{
    public class QueuedHostedService : BackgroundService
    {
        public QueuedHostedService(IBackgroundTaskQueue taskQueue)
        {
            TaskQueue = taskQueue;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected async override Task ExecuteAsync( CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);

                try
                {
                    await workItem(cancellationToken);
                }
                catch (Exception ex)
                {
                    Logger.LogException($"Error occurred executing {nameof(workItem)}", ex);
                }
            }

            Logger.LogInfo("Queued Hosted Service is stopping.");
        }
    }
}

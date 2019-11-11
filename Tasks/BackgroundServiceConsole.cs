using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aspnet_core_warmup.Tasks
{
    public class BackgroundServiceConsole : BackgroundService
    {
        private readonly ILogger<BackgroundServiceConsole> _logger;

        public BackgroundServiceConsole(ILogger<BackgroundServiceConsole> logger)
        {
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            // this will be executed in parallel with the other asp.net core services
            _logger.LogDebug("Starting the background work");
            return base.StartAsync(cancellationToken);
        }
        protected override /*async*/ Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested) // if the request is not cancelled
            {
                // do something, that will be executed over and over
                _logger.LogDebug($"************* Worker running at: {DateTime.Now} *****************");
            }
            // the task has been executed one 1. Marking it as completed means we won't be able to
            // executed this again.
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // the worker will not be disposed until the web app is done
            _logger.LogDebug("Finishing / cleaning up the background work");
            return Task.CompletedTask;
        }
    }
}

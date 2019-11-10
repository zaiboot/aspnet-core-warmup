using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aspnet_core_warmup
{
    public class BackgroundServiceConsole : BackgroundService
    {
        private readonly ILogger<BackgroundServiceConsole> _logger;
        
        public BackgroundServiceConsole(ILogger<BackgroundServiceConsole> logger)
        {
            this._logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the background work");
            return base.StartAsync(cancellationToken);
        }
        protected override /*async*/ Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // while (!stoppingToken.IsCancellationRequested)
            // {
            _logger.LogDebug($"************* Worker running at: {DateTime.Now} *****************");
            // await Task.Delay(1000, stoppingToken);
            return Task.CompletedTask;
            // }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finishing / cleaning up the background work");
            return Task.CompletedTask;
        }
    }
}

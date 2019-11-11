using System.Threading;
using System.Threading.Tasks;
using aspnet_core_warmup.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aspnet_core_warmup.Tasks
{
    public class MigrationHostedService : IHostedService
    {
        private readonly ILogger<MigrationHostedService> _logger;
        private readonly WarmUpContext _context;

        public MigrationHostedService(ILogger<MigrationHostedService> logger, WarmUpContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Initiating migration process");
            await _context.MigrateAsync();
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Are migrations done? Should we ");
            return Task.CompletedTask;
        }
    }
}

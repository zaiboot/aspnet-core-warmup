using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

namespace aspnet_core_warmup.Database
{
    public class WarmUpContext : DbContext
    {
        private readonly ILogger<WarmUpContext> _logger;

        public DbSet<TestTable> TestTable { get; set; }
        public WarmUpContext([NotNull] DbContextOptions options, ILogger<WarmUpContext> logger) : base(options)
        {
            this._logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("testing");
        }
        
        public async Task MigrateAsync()
        {
            await Database.GetPendingMigrationsAsync().ContinueWith(async pm =>
            {
                if (pm.IsCompletedSuccessfully && pm.Result.Any())
                {
                    await Database.MigrateAsync();
                }
            });
        }
    }
}


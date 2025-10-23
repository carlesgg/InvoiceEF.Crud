using InvoiceEF.Crud.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ForbesSeeder(IServiceProvider serviceProvider, ILogger<ForbesSeeder> logger) : IHostedService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<ForbesSeeder> _logger = logger;

        private static bool _hasRun = false;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_hasRun) return;
            _hasRun = true;

            using var scope = _serviceProvider.CreateScope();
            var forbesService = scope.ServiceProvider.GetRequiredService<IForbesService>();

            // Get billionaires
            var dtosResult = await forbesService.GetBillionairesAsync(cancellationToken);

            if (dtosResult.HasErrors || dtosResult.Result == null)
            {
                _logger.LogError(
                    "Failed to fetch Forbes billionaires: {Errors}",
                    string.Join(", ", dtosResult.Errors.Select(e => e.Message))
                );
                return;
            }

            // Pass the Result (the IEnumerable<ForbesPersonDto>) to DeleteAllAndSeedAsync
            var seedResult = await forbesService.DeleteAllAndSeedAsync(dtosResult, cancellationToken);

            if (seedResult.HasErrors)
            {
                _logger.LogError(
                    "Failed to seed Forbes billionaires: {Errors}",
                    string.Join(", ", seedResult.Errors.Select(e => e.Message))
                );
                return;
            }

            _logger.LogInformation("Starting ForbesSeeder. Scope ID: {ScopeId}", Guid.NewGuid());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}

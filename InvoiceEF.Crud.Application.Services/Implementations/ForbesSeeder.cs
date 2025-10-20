using InvoiceEF.Crud.Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvoiceEF.Crud.Application.Services.Implementations
{
    public class ForbesSeeder(IServiceProvider serviceProvider) : IHostedService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var forbesService = scope.ServiceProvider.GetRequiredService<IForbesService>();
            var dtos = await forbesService.GetBillionairesAsync(cancellationToken);
            await forbesService.SaveBillionairesAsync(dtos, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var forbesService = scope.ServiceProvider.GetRequiredService<IForbesService>();
            await forbesService.DropDatabaseAsync(cancellationToken);
        }
    }
}

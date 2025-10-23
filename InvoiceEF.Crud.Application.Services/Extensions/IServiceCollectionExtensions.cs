using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Application.Services.Implementations;
using InvoiceEF.Crud.Application.Mappers.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace InvoiceEF.Crud.Application.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServicesLayer();
            services.AddHealthChecksService(configuration);


        }

        public static void AddServicesLayer(this IServiceCollection services)
        {
            // Register services here
            services
                .AddScoped<IClientService, ClientService>()
                .AddScoped<ICompanyService, CompanyService>()
                .AddScoped<IInvoiceService, InvoiceService>()
                .AddScoped<IInvoiceLineService, InvoiceLineService>()
                .AddScoped<IForbesService, ForbesService>();

            services 
                .AddHostedService<ForbesSeeder>();

        }

        public static void AddHealthChecksService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("Api", () => HealthCheckResult.Healthy("API is running"))
                .AddSqlServer(
                    connectionString: configuration.GetConnectionString("InvoiceDocker2")!,
                    healthQuery: "SELECT 1;",
                    failureStatus: HealthStatus.Unhealthy,
                    timeout: TimeSpan.FromSeconds(5),
                    tags: new[] { "Database" })
                .AddRedis(
                    redisConnectionString: configuration.GetConnectionString("RedisConnection")!,
                    failureStatus: HealthStatus.Unhealthy,
                    timeout: TimeSpan.FromSeconds(3),
                    tags: new[] { "Inmemory Database" });
        }
    }
}

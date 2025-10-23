using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System.Reflection;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProxies(this IServiceCollection services, IConfiguration configuration)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            services.AddHttpClient("ForbesProxy", client =>
            {
                client.BaseAddress = new Uri(configuration["Proxies:Forbes"]!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(retryPolicy);

            services.AddScoped<IForbesProxy, ForbesProxy>();

            return services;
        }
    }
}

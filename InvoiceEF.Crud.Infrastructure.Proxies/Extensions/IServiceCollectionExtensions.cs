using InvoiceEF.Crud.Infrastructure.Proxies.Contracts;
using InvoiceEF.Crud.Infrastructure.Proxies.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace InvoiceEF.Crud.Infrastructure.Proxies.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProxies(this IServiceCollection services)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            services.AddHttpClient<IForbesProxy, ForbesProxy>(client =>
            {
                client.BaseAddress = new Uri("https://forbes-api.vercel.app/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}

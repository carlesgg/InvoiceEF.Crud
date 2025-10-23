using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceEF.Crud.Api.HealthChecks
{
    public static class HealthCheckUISetup
    {
        public static void AddHealthChecksUISetup(this IServiceCollection services)
        {
            // Register the UI and in-memory storage
            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);   
                options.MaximumHistoryEntriesPerEndpoint(60);
                options.AddHealthCheckEndpoint("Invoice API", "/health/ready"); 
            })
            .AddInMemoryStorage(); 
        }

        public static void MapHealthChecksUISetup(this WebApplication app)
        {
            // Live Endpoint
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false, 
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ready Endpoint
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = _ => true, // include all registered checks
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Visual Endpoint
            app.MapHealthChecksUI(options =>
            {
                options.UIPath = "/health-ui";       // browser path
                options.ApiPath = "/health-ui-api";  // API path the UI polls
            });
        }
    }
}

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
                options.SetEvaluationTimeInSeconds(15);   // refresh interval
                options.MaximumHistoryEntriesPerEndpoint(60);
                options.AddHealthCheckEndpoint("Invoice API", "/health/ready"); // endpoint to monitor
            })
            .AddInMemoryStorage(); // required!
        }

        public static void MapHealthChecksUISetup(this WebApplication app)
        {
            // Liveness endpoint
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false, // only self-check
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Readiness endpoint (includes all checks)
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = _ => true, // include all registered checks
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Map the visual dashboard
            app.MapHealthChecksUI(options =>
            {
                options.UIPath = "/health-ui";       // browser path
                options.ApiPath = "/health-ui-api";  // API path the UI polls
            });
        }
    }
}

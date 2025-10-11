using Microsoft.Extensions.DependencyInjection;
using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Application.Services.Implementations;

namespace InvoiceEF.Crud.Application.Services.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddServicesLayer();
        }

        public static void AddServicesLayer(this IServiceCollection services)
        {
            // Register services here
            // e.g., services.AddTransient<IMyOtherService, MyOtherService>();
            services
                .AddScoped<IStudentService, StudentService>();
        }
    }
}

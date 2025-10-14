using InvoiceEF.Crud.Application.Services.Contracts;
using InvoiceEF.Crud.Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

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
            services
                .AddScoped<IClientService, ClientService>()
                .AddScoped<ICompanyService, CompanyService>()
                .AddScoped<IInvoiceService, InvoiceService>()
                .AddScoped<IInvoiceLineService, InvoiceLineService>();

        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoriesLayer(connectionString);
        }

        public static void AddRepositoriesLayer(this IServiceCollection services, string connectionString)
        {
            // Register services here
            services
                .AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<ICompanyRepository, CompanyRepository>()
                .AddScoped<IInvoiceRepository, InvoiceRepository>()
                .AddScoped<IInvoiceLineRepository, InvoiceLineRepository>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}

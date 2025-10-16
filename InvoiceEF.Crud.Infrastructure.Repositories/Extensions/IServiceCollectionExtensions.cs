using InvoiceEF.Crud.Domain.Contracts;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Base.Contracts;
using InvoiceEF.Crud.Infrastructure.Base.Implementations;
using InvoiceEF.Crud.Infrastructure.Context.Implementations;
using InvoiceEF.Crud.Infrastructure.Data;
using InvoiceEF.Crud.Infrastructure.Mappers;
using InvoiceEF.Crud.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<IInvoiceLineRepository, InvoiceLineRepository>()

                .AddScoped<IMapper<ClientEntity, Client>, ClientMapper>()
                .AddScoped<IMapper<CompanyEntity, Company>, CompanyMapper>()
                .AddScoped<IMapper<InvoiceEntity, Invoice>, InvoiceMapper>()
                .AddScoped<IMapper<InvoiceLineEntity, InvoiceLine>, InvoiceLineMapper>()

                .AddScoped<IUnitOfWork, UnitOfWork>()

                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString));
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using InvoiceEF.Crud.Infrastructure.Repositories.Contracts;
using InvoiceEF.Crud.Infrastructure.Repositories.Implementations;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddRepositoriesLayer();
        }

        public static void AddRepositoriesLayer(this IServiceCollection services)
        {
            // Register services here
            // e.g., services.AddTransient<IMyOtherService, MyOtherService>();
            services
                .AddScoped<IStudentRepository, StudentRepository>();
            services
                .AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        }
    }
}

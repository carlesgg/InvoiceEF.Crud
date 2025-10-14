using Microsoft.EntityFrameworkCore;
using InvoiceEF.Crud.Domain.Entities;
using InvoiceEF.Crud.Infrastructure.Repositories.Data;

namespace InvoiceEF.Crud.Infrastructure.Context.Implementations
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }
    }
}

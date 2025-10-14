using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Data
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(s => s.CompanyId);

            builder.Property(s => s.CompanyId)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Direction)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Email)
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .HasMaxLength(20);
        }
    }
}

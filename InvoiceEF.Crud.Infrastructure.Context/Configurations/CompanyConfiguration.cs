using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Infrastructure.Context.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
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

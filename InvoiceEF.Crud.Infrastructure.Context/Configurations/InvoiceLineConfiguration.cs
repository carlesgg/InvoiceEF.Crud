using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Infrastructure.Context.Configurations
{
    public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            // Table name
            builder.ToTable("InvoiceLines");

            // Primary key
            builder.HasKey(l => l.LineId);

            // Properties
            builder.Property(l => l.LineId)
                   .IsRequired();

            builder.Property(l => l.InvoiceId)
                   .IsRequired();

            builder.Property(l => l.Concept)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(l => l.Quantity)
                   .IsRequired();

            builder.Property(l => l.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(l => l.LineTotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

        }
    }
}

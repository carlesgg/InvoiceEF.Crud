using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvoiceEF.Crud.Domain.Entities;

namespace InvoiceEF.Crud.Infrastructure.Repositories.Data
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            // Table name
            builder.ToTable("Invoices");

            // Primary key
            builder.HasKey(i => i.InvoiceId);

            // Properties
            builder.Property(i => i.InvoiceId)
                   .IsRequired();

            builder.Property(i => i.ClientId)
                   .IsRequired();

            builder.Property(i => i.CompanyId)
                   .IsRequired();

            builder.Property(i => i.InvoiceDate)
                   .IsRequired();

            builder.Property(i => i.Estimate)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(i => i.Signature)
                   .HasMaxLength(500);

            // Relationships
            builder.HasMany(i => i.Lines)
                   .WithOne()
                   .HasForeignKey(l => l.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade); // deleting an invoice deletes lines
        }
    }
}

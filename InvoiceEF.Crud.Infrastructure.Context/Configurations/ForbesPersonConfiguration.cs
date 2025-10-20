using InvoiceEF.Crud.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceEF.Crud.Infrastructure.Context.Configurations
{
    public class ForbesPersonConfiguration : IEntityTypeConfiguration<ForbesPerson>
    {
        public void Configure(EntityTypeBuilder<ForbesPerson> builder)
        {
            // Nombre de la tabla
            builder.ToTable("ForbesPeople");

            // Clave primaria
            builder.HasKey(f => f.Id);

            // Propiedades
            builder.Property(f => f.Uri)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.PersonName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(f => f.ListUri)
                .HasMaxLength(100);

            builder.Property(f => f.Source)
                .HasMaxLength(200);

            builder.Property(f => f.Industries)
                .HasMaxLength(500);

            builder.Property(f => f.CountryOfCitizenship)
                .HasMaxLength(100);

            builder.Property(f => f.Gender)
                .HasMaxLength(10);

            builder.Property(f => f.LastName)
                .HasMaxLength(100);

            builder.Property(f => f.SquareImage)
                .HasMaxLength(500);

            // Propiedades numéricas
            builder.Property(f => f.Rank);
            builder.Property(f => f.FinalWorth);
            builder.Property(f => f.EstWorthPrev);

            // Fecha opcional
            builder.Property(f => f.BirthDate);
        }
    }
}

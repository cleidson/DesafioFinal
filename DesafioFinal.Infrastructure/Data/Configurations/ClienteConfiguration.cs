using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DesafioFinal.Core.Models;

namespace DesafioFinal.Infrastructure.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .IsRequired();

            builder.Property(s => s.Nome)
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            builder.Property(s => s.Email)
                               .HasColumnType("varchar(255)")
                               .IsRequired();
        }
    }
}

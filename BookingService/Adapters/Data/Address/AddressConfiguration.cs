using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Address
{
    public class AddressConfiguration : IEntityTypeConfiguration<Domain.Order.Entities.Address>
    {
        public void Configure(EntityTypeBuilder<Domain.Order.Entities.Address> builder)
        {
            // Define a chave primária
            builder.HasKey(e => e.Id);

            // Mapeamento de propriedades simples
            builder.Property(e => e.Street)
                .HasMaxLength(200);

            builder.Property(e => e.City)
                .HasMaxLength(100);

            builder.Property(e => e.PostalCode)
                .HasMaxLength(20);

            builder.Property(e => e.Number)
                .IsRequired();

            // Índice único para PostalCode e Number
            builder.HasIndex(e => new { e.PostalCode, e.Number })
                .IsUnique();

            // Nome da tabela no banco de dados
            builder.ToTable("Addresses");
        }
    }
}

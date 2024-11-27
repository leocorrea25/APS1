using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Order.Entities;

namespace Data.Order
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Order.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order.Entities.Order> builder)
        {
            // Define a chave primária
            builder.HasKey(e => e.Id);

            // Mapeamento de propriedades simples
            builder.Property(e => e.DeliveryOption)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.Property(e => e.ContactName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ContactPhone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.ContactEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.PreferredDate)
                .IsRequired();

            builder.Property(e => e.PreferredTime)
                .IsRequired();

            builder.Property(e => e.AdditionalInstructions)
                .HasMaxLength(500);

            builder.Property(e => e.IsCompleted)
                .IsRequired();

            // Configuração do tipo de propriedade composta Address
            builder.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasMaxLength(200);

                address.Property(a => a.City)
                    .HasMaxLength(100);

                address.Property(a => a.PostalCode)
                    .HasMaxLength(20);
            });

            // Nome da tabela no banco de dados
            builder.ToTable("Orders");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Booking
{
    public class BookingConfiguration : IEntityTypeConfiguration<Domain.Entities.Booking>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Booking> builder)
        {
            // Definindo a chave primária
            builder.HasKey(b => b.Id);

            // Configurando propriedades
            builder.Property(b => b.PlacedAt)
                   .IsRequired();

            builder.Property(b => b.Start)
                   .IsRequired();

            builder.Property(b => b.End)
                   .IsRequired();

            // Configurando relacionamentos
            builder.HasOne(b => b.Room)
                   .WithMany()
                   .HasForeignKey("RoomId");

            builder.HasOne(b => b.Guest)
                   .WithMany()
                   .HasForeignKey("GuestId");

            // Configurando enum como string
            builder.Property(b => b.Status)
                   .HasConversion<string>();
        }
    }
}

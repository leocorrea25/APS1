using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Data.Order
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DeliveryOption)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.AdditionalInstructions)
                .HasMaxLength(500);

            builder.Property(e => e.IsCompleted)
                .IsRequired();

            builder.Property(e => e.ProductQuantity)
                .IsRequired();

            builder.HasOne(e => e.Address)
                .WithMany()
                .HasForeignKey(e => e.AddressId)
                .OnDelete(DeleteBehavior.NoAction); // Evitar cascata no Address


            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); // Evitar cascata no User

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); // Evitar cascata no Product

            builder.ToTable("Orders");
        }
    }
}

using Data.Address;
using Data.Booking;
using Data.Guest;
using Data.Order;
using Data.Product;
using Data.Room;
using Data.User;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public virtual DbSet<Domain.Entities.Guest> Guests { get; set; }
        public virtual DbSet<Domain.Entities.Booking> Bokings { get; set; }
        public virtual DbSet<Domain.Entities.Room> Rooms { get; set; }
        public virtual DbSet<Domain.Order.Entities.Order> Orders { get; set; }
        public virtual DbSet<Domain.Order.Entities.User> Users { get; set; }
        public virtual DbSet<Domain.Order.Entities.Product> Products { get; set; }
        public virtual DbSet<Domain.Order.Entities.Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
        }
    }
}

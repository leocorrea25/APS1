using Data.Address;
using Data.Order;
using Data.Product;
using Data.User;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public virtual DbSet<Domain.Entities.Order> Orders { get; set; }
        public virtual DbSet<Domain.Entities.User> Users { get; set; }
        public virtual DbSet<Domain.Entities.Product> Products { get; set; }
        public virtual DbSet<Domain.Entities.Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
        }
    }
}

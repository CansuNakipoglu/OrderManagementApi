using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Entities;
using OrderManagement.Data.Contexts.Abstracts;

namespace OrderManagement.Data.Contexts
{
    public class PostgreDbContext : DbContext, IDbContext
    {
        public PostgreDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Müşteri - Sipariş | Bire Çok
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order ve Product arasında doğrudan çoka çok ilişki tanımlanması
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<int>();
        }
    }
}

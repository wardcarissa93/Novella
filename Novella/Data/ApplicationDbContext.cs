using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Novella.Models;

namespace Novella.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductCart> ProductCarts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Include IdentityDbContext configurations

            // Configurations for ProductCart (Many-to-Many relationship between Product and Cart)
            modelBuilder.Entity<ProductCart>()
                .HasKey(pc => new { pc.CartId, pc.ProductId });

            modelBuilder.Entity<ProductCart>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCart>()
                .HasOne(pc => pc.Cart)
                .WithMany(c => c.ProductCarts)
                .HasForeignKey(pc => pc.CartId);

            // Product and Category (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Product and ProductOrder (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductOrders)
                .WithOne(po => po.Product)
                .HasForeignKey(po => po.ProductId);

            // Order and ProductOrder (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.ProductOrders)
                .WithOne(po => po.Order)
                .HasForeignKey(po => po.OrderId);

            // UserAccount and Cart (One-to-Many)
            modelBuilder.Entity<UserAccount>()
                .HasMany(ua => ua.Carts)
                .WithOne(c => c.UserAccount)
                .HasForeignKey(c => c.UserId);

            // UserAccount and Order (One-to-Many)
            modelBuilder.Entity<UserAccount>()
                .HasMany(ua => ua.Orders)
                .WithOne(o => o.UserAccount)
                .HasForeignKey(o => o.UserId);

            // UserAccount and Rating (One-to-Many)
            modelBuilder.Entity<UserAccount>()
                .HasMany(ua => ua.Ratings)
                .WithOne(r => r.UserAccount)
                .HasForeignKey(r => r.UserId);

            // Product and Rating (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);

            // UserAccount, Address (One-to-Many for Shipping and Billing)
            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.ShippingAddress)
                .WithMany(a => a.ShippingUserAccounts)
                .HasForeignKey(ua => ua.ShippingId);

            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.BillingAddress)
                .WithMany(a => a.BillingUserAccounts)
                .HasForeignKey(ua => ua.BillingId);

            // Order and OrderStatus (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany(os => os.Orders)
                .HasForeignKey(o => o.OrderStatusId);
        }
    }
}
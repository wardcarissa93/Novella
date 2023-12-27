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

            // UserAccount to Address (One-to-One)
            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.ShippingAddress)
                .WithMany()
                .HasForeignKey(ua => ua.ShippingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.BillingAddress)
                .WithMany()
                .HasForeignKey(ua => ua.BillingId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserAccount to Cart (One-to-One)
            modelBuilder.Entity<UserAccount>()
                .HasOne(ua => ua.Cart)
                .WithOne()
                .HasForeignKey<Cart>(c => c.UserId);

            // UserAccount to Order (One-to-Many)
            modelBuilder.Entity<UserAccount>()
                .HasMany(ua => ua.Orders)
                .WithOne(o => o.UserAccount)
                .HasForeignKey(o => o.UserId);

            // UserAccount to Rating (One-to-Many)
            modelBuilder.Entity<UserAccount>()
                .HasMany(ua => ua.Ratings)
                .WithOne(r => r.UserAccount)
                .HasForeignKey(r => r.UserId);

            // Category to Product (One-to-Many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Product to ProductCart (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductCarts)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId);

            // Product to ProductOrder (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductOrders)
                .WithOne(po => po.Product)
                .HasForeignKey(po => po.ProductId);

            // Product to Rating (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);

            // Cart to ProductCart (One-to-Many)
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.ProductCarts)
                .WithOne(pc => pc.Cart)
                .HasForeignKey(pc => pc.CartId);

            // Order to ProductOrder (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.ProductOrders)
                .WithOne(po => po.Order)
                .HasForeignKey(po => po.OrderId);

            // OrderStatus to Order (One-to-Many)
            modelBuilder.Entity<OrderStatus>()
                .HasMany(os => os.Orders)
                .WithOne(o => o.OrderStatus)
                .HasForeignKey(o => o.OrderStatusId);
        }
    }
}
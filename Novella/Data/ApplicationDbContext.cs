using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Novella.EfModels;

namespace Novella.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // If UserAccount, Address, Order, OrderStatus are related to Identity and not duplicated in NovellaContext, keep them
        //public DbSet<UserAccount> UserAccounts { get; set; } // Keep if UserAccount extends IdentityUser
        //public DbSet<Address> Addresses { get; set; } // Keep if needed for user addresses
        //public DbSet<Order> Orders { get; set; } // Keep if these orders are related to the authentication/identity context
        //public DbSet<OrderStatus> OrderStatuses { get; set; } // Keep if necessary for order processing in the identity context

        // Remove DbSet properties that are being managed by NovellaContext to avoid duplication
        // Example of removed DbSets:
        // public DbSet<Cart> Carts { get; set; }
        // public DbSet<ProductCart> ProductCarts { get; set; }
        // public DbSet<Product> Products { get; set; }
        // public DbSet<ProductOrder> ProductOrders { get; set; }
        // public DbSet<Category> Categories { get; set; }
        // public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Include IdentityDbContext configurations

            // Keep or modify configurations that are directly related to the ASP.NET Identity
            // or unique entities within this context. Remove configurations managed by NovellaContext.

            // Example configuration to keep if UserAccount extends IdentityUser and has unique relationships here
            // modelBuilder.Entity<UserAccount>()
            //     .HasOne(ua => ua.ShippingAddress)
            //     .WithMany()
            //     .HasForeignKey(ua => ua.ShippingId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // Further configurations...

        }
    }
}

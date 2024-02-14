using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Novella.EfModels;

public partial class NovellaContext : DbContext
{
    public NovellaContext()
    {
    }

    public NovellaContext(DbContextOptions<NovellaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCart> ProductCarts { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:freeazuresqlservercarissa.database.windows.net,1433;Initial Catalog=Novella;Persist Security Info=False;User ID=CarissaWard;Password=P@ssw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.PkAddressId).HasName("PK__Address__70806256AACC0CB1");

            entity.ToTable("Address");

            entity.Property(e => e.PkAddressId)
                .ValueGeneratedNever()
                .HasColumnName("pkAddressId");
            entity.Property(e => e.AddressLineOne)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("addressLineOne");
            entity.Property(e => e.AddressLineTwo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("addressLineTwo");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("postalCode");
            entity.Property(e => e.Province)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("province");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.PkCartId).HasName("PK__Cart__07D27F95AC63F6F0");

            entity.ToTable("Cart");

            entity.Property(e => e.PkCartId)
                .ValueGeneratedNever()
                .HasColumnName("pkCartId");
            entity.Property(e => e.FkUserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fkUserId");

            entity.HasOne(d => d.FkUser).WithMany(p => p.Carts)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__fkUserId__6C190EBB");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.PkCategoryId).HasName("PK__Category__9457381F4B965F2D");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryDescription, "UQ__Category__0007481DDDE4E047").IsUnique();

            entity.HasIndex(e => e.CategoryName, "UQ__Category__37077ABD15FE1CDE").IsUnique();

            entity.Property(e => e.PkCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("pkCategoryId");
            entity.Property(e => e.CategoryDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("categoryDescription");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.PkOrderId).HasName("PK__Order__C196130B2F15988C");

            entity.ToTable("Order");

            entity.Property(e => e.PkOrderId)
                .ValueGeneratedNever()
                .HasColumnName("pkOrderId");
            entity.Property(e => e.DateOrdered)
                .HasColumnType("datetime")
                .HasColumnName("dateOrdered");
            entity.Property(e => e.FkBillingAddressId).HasColumnName("fkBillingAddressId");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("fkOrderStatusId");
            entity.Property(e => e.FkShippingAddressId).HasColumnName("fkShippingAddressId");
            entity.Property(e => e.FkUserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fkUserId");
            entity.Property(e => e.PaypalTransactionId)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("paypalTransactionId");

            entity.HasOne(d => d.FkBillingAddress).WithMany(p => p.OrderFkBillingAddresses)
                .HasForeignKey(d => d.FkBillingAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__fkBilling__7F2BE32F");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkOrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__fkOrderSt__7D439ABD");

            entity.HasOne(d => d.FkShippingAddress).WithMany(p => p.OrderFkShippingAddresses)
                .HasForeignKey(d => d.FkShippingAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__fkShippin__7E37BEF6");

            entity.HasOne(d => d.FkUser).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order__fkUserId__7C4F7684");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.PkOrderStatusId).HasName("PK__OrderSta__ABDB6887AE43DF56");

            entity.ToTable("OrderStatus");

            entity.HasIndex(e => e.StatusName, "UQ__OrderSta__6A50C212A8552E14").IsUnique();

            entity.HasIndex(e => e.StatusDescription, "UQ__OrderSta__B1D85D3F9C6D1F4A").IsUnique();

            entity.Property(e => e.PkOrderStatusId)
                .ValueGeneratedNever()
                .HasColumnName("pkOrderStatusId");
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("statusDescription");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("statusName");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PkProductId).HasName("PK__Product__4492A4B549F021F0");

            entity.ToTable("Product");

            entity.Property(e => e.PkProductId)
                .ValueGeneratedNever()
                .HasColumnName("pkProductId");
            entity.Property(e => e.FkCategoryId).HasColumnName("fkCategoryId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("productDescription");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("productName");
            entity.Property(e => e.QuantityAvailable).HasColumnName("quantityAvailable");

            entity.HasOne(d => d.FkCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__fkCateg__6FE99F9F");
        });

        modelBuilder.Entity<ProductCart>(entity =>
        {
            entity.HasKey(e => e.PkProductCartId).HasName("PK__ProductC__B08CDA1C902E01C7");

            entity.ToTable("ProductCart");

            entity.Property(e => e.PkProductCartId)
                .ValueGeneratedNever()
                .HasColumnName("pkProductCartId");
            entity.Property(e => e.FkCartId).HasColumnName("fkCartId");
            entity.Property(e => e.FkProductId).HasColumnName("fkProductId");
            entity.Property(e => e.QuantityInCart).HasColumnName("quantityInCart");

            entity.HasOne(d => d.FkCart).WithMany(p => p.ProductCarts)
                .HasForeignKey(d => d.FkCartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCa__fkCar__72C60C4A");

            entity.HasOne(d => d.FkProduct).WithMany(p => p.ProductCarts)
                .HasForeignKey(d => d.FkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCa__fkPro__73BA3083");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.PkProductOrderId).HasName("PK__ProductO__17747A35F0FF20CC");

            entity.ToTable("ProductOrder");

            entity.Property(e => e.PkProductOrderId)
                .ValueGeneratedNever()
                .HasColumnName("pkProductOrderId");
            entity.Property(e => e.FkOrderId).HasColumnName("fkOrderId");
            entity.Property(e => e.FkProductId).HasColumnName("fkProductId");
            entity.Property(e => e.QuantityInOrder).HasColumnName("quantityInOrder");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductOr__fkOrd__02084FDA");

            entity.HasOne(d => d.FkProduct).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.FkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductOr__fkPro__02FC7413");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.PkRatingId).HasName("PK__Rating__38287FB2B9A9BCC4");

            entity.ToTable("Rating");

            entity.Property(e => e.PkRatingId)
                .ValueGeneratedNever()
                .HasColumnName("pkRatingId");
            entity.Property(e => e.DateRated)
                .HasColumnType("datetime")
                .HasColumnName("dateRated");
            entity.Property(e => e.FkProductId).HasColumnName("fkProductId");
            entity.Property(e => e.FkUserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fkUserId");
            entity.Property(e => e.Rating1)
                .HasColumnType("decimal(2, 1)")
                .HasColumnName("rating");
            entity.Property(e => e.Review)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("review");

            entity.HasOne(d => d.FkProduct).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.FkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rating__fkProduc__778AC167");

            entity.HasOne(d => d.FkUser).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rating__fkUserId__787EE5A0");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.PkUserId).HasName("PK__UserAcco__1790FCDFA3D042B2");

            entity.ToTable("UserAccount");

            entity.Property(e => e.PkUserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pkUserId");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.PaypalAccount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("paypalAccount");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

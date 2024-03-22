﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Novella.EfModels;

#nullable disable

namespace Novella.Migrations
{
    [DbContext(typeof(NovellaContext))]
    partial class NovellaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Novella.EfModels.Address", b =>
                {
                    b.Property<int>("PkAddressId")
                        .HasColumnType("int")
                        .HasColumnName("pkAddressId");

                    b.Property<string>("AddressLineOne")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("addressLineOne");

                    b.Property<string>("AddressLineTwo")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("addressLineTwo");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("city");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("varchar(6)")
                        .HasColumnName("postalCode");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("province");

                    b.HasKey("PkAddressId")
                        .HasName("PK__Address__70806256AACC0CB1");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.Cart", b =>
                {
                    b.Property<int>("PkCartId")
                        .HasColumnType("int")
                        .HasColumnName("pkCartId");

                    b.Property<string>("FkUserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("fkUserId");

                    b.HasKey("PkCartId")
                        .HasName("PK__Cart__07D27F95AC63F6F0");

                    b.HasIndex("FkUserId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.Category", b =>
                {
                    b.Property<int>("PkCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("pkCategoryId");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("categoryDescription");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("categoryName");

                    b.HasKey("PkCategoryId")
                        .HasName("PK__Category__9457381F4B965F2D");

                    b.HasIndex(new[] { "CategoryDescription" }, "UQ__Category__0007481DDDE4E047")
                        .IsUnique();

                    b.HasIndex(new[] { "CategoryName" }, "UQ__Category__37077ABD15FE1CDE")
                        .IsUnique();

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.ImageStore", b =>
                {
                    b.Property<int?>("PkImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkImageId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PkImageId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("fileName");

                    b.Property<int?>("FkProductId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("fkProductId");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("image");

                    b.HasKey("PkImageId");

                    b.HasIndex("FkProductId");

                    b.HasIndex(new[] { "FileName" }, "UQ__ImageSto__431DED434E59CA40")
                        .IsUnique();

                    b.ToTable("ImageStore", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.Order", b =>
                {
                    b.Property<int>("PkOrderId")
                        .HasColumnType("int")
                        .HasColumnName("pkOrderId");

                    b.Property<DateTime>("DateOrdered")
                        .HasColumnType("datetime")
                        .HasColumnName("dateOrdered");

                    b.Property<int>("FkBillingAddressId")
                        .HasColumnType("int")
                        .HasColumnName("fkBillingAddressId");

                    b.Property<int>("FkOrderStatusId")
                        .HasColumnType("int")
                        .HasColumnName("fkOrderStatusId");

                    b.Property<int>("FkShippingAddressId")
                        .HasColumnType("int")
                        .HasColumnName("fkShippingAddressId");

                    b.Property<string>("FkUserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("fkUserId");

                    b.Property<string>("PaypalTransactionId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)")
                        .HasColumnName("paypalTransactionId");

                    b.HasKey("PkOrderId")
                        .HasName("PK__Order__C196130B2F15988C");

                    b.HasIndex("FkBillingAddressId");

                    b.HasIndex("FkOrderStatusId");

                    b.HasIndex("FkShippingAddressId");

                    b.HasIndex("FkUserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.OrderStatus", b =>
                {
                    b.Property<int>("PkOrderStatusId")
                        .HasColumnType("int")
                        .HasColumnName("pkOrderStatusId");

                    b.Property<string>("StatusDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("statusDescription");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("statusName");

                    b.HasKey("PkOrderStatusId")
                        .HasName("PK__OrderSta__ABDB6887AE43DF56");

                    b.HasIndex(new[] { "StatusName" }, "UQ__OrderSta__6A50C212A8552E14")
                        .IsUnique();

                    b.HasIndex(new[] { "StatusDescription" }, "UQ__OrderSta__B1D85D3F9C6D1F4A")
                        .IsUnique();

                    b.ToTable("OrderStatus", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.Product", b =>
                {
                    b.Property<int>("PkProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkProductId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PkProductId"));

                    b.Property<int>("FkCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("fkCategoryId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6, 2)")
                        .HasColumnName("price");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("productDescription");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("productName");

                    b.Property<int>("QuantityAvailable")
                        .HasColumnType("int")
                        .HasColumnName("quantityAvailable");

                    b.HasKey("PkProductId")
                        .HasName("PK__Product__4492A4B549F021F0");

                    b.HasIndex("FkCategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.ProductCart", b =>
                {
                    b.Property<int>("PkProductCartId")
                        .HasColumnType("int")
                        .HasColumnName("pkProductCartId");

                    b.Property<int>("FkCartId")
                        .HasColumnType("int")
                        .HasColumnName("fkCartId");

                    b.Property<int>("FkProductId")
                        .HasColumnType("int")
                        .HasColumnName("fkProductId");

                    b.Property<int>("QuantityInCart")
                        .HasColumnType("int")
                        .HasColumnName("quantityInCart");

                    b.HasKey("PkProductCartId")
                        .HasName("PK__ProductC__B08CDA1C902E01C7");

                    b.HasIndex("FkCartId");

                    b.HasIndex("FkProductId");

                    b.ToTable("ProductCart", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.ProductOrder", b =>
                {
                    b.Property<int>("PkProductOrderId")
                        .HasColumnType("int")
                        .HasColumnName("pkProductOrderId");

                    b.Property<int>("FkOrderId")
                        .HasColumnType("int")
                        .HasColumnName("fkOrderId");

                    b.Property<int>("FkProductId")
                        .HasColumnType("int")
                        .HasColumnName("fkProductId");

                    b.Property<int>("QuantityInOrder")
                        .HasColumnType("int")
                        .HasColumnName("quantityInOrder");

                    b.HasKey("PkProductOrderId")
                        .HasName("PK__ProductO__17747A35F0FF20CC");

                    b.HasIndex("FkOrderId");

                    b.HasIndex("FkProductId");

                    b.ToTable("ProductOrder", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.Rating", b =>
                {
                    b.Property<int>("PkRatingId")
                        .HasColumnType("int")
                        .HasColumnName("pkRatingId");

                    b.Property<DateTime>("DateRated")
                        .HasColumnType("datetime")
                        .HasColumnName("dateRated");

                    b.Property<int>("FkProductId")
                        .HasColumnType("int")
                        .HasColumnName("fkProductId");

                    b.Property<string>("FkUserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("fkUserId");

                    b.Property<decimal>("RatingValue")
                        .HasColumnType("decimal(2, 1)")
                        .HasColumnName("rating");

                    b.Property<string>("Review")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("review");

                    b.HasKey("PkRatingId")
                        .HasName("PK__Rating__38287FB2B9A9BCC4");

                    b.HasIndex("FkProductId");

                    b.HasIndex("FkUserId");

                    b.ToTable("Rating", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.UserAccount", b =>
                {
                    b.Property<string>("PkUserId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("pkUserId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("lastName");

                    b.Property<string>("PaypalAccount")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("paypalAccount");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("phoneNumber");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("role");

                    b.HasKey("PkUserId")
                        .HasName("PK__UserAcco__1790FCDFA3D042B2");

                    b.ToTable("UserAccount", (string)null);
                });

            modelBuilder.Entity("Novella.EfModels.Cart", b =>
                {
                    b.HasOne("Novella.EfModels.UserAccount", "FkUser")
                        .WithMany("Carts")
                        .HasForeignKey("FkUserId")
                        .IsRequired()
                        .HasConstraintName("FK__Cart__fkUserId__6C190EBB");

                    b.Navigation("FkUser");
                });

            modelBuilder.Entity("Novella.EfModels.ImageStore", b =>
                {
                    b.HasOne("Novella.EfModels.Product", "FkProduct")
                        .WithMany("ImageStores")
                        .HasForeignKey("FkProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__ImageStor__fkPro__09746778");

                    b.Navigation("FkProduct");
                });

            modelBuilder.Entity("Novella.EfModels.Order", b =>
                {
                    b.HasOne("Novella.EfModels.Address", "FkBillingAddress")
                        .WithMany("OrderFkBillingAddresses")
                        .HasForeignKey("FkBillingAddressId")
                        .IsRequired()
                        .HasConstraintName("FK__Order__fkBilling__7F2BE32F");

                    b.HasOne("Novella.EfModels.OrderStatus", "FkOrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("FkOrderStatusId")
                        .IsRequired()
                        .HasConstraintName("FK__Order__fkOrderSt__7D439ABD");

                    b.HasOne("Novella.EfModels.Address", "FkShippingAddress")
                        .WithMany("OrderFkShippingAddresses")
                        .HasForeignKey("FkShippingAddressId")
                        .IsRequired()
                        .HasConstraintName("FK__Order__fkShippin__7E37BEF6");

                    b.HasOne("Novella.EfModels.UserAccount", "FkUser")
                        .WithMany("Orders")
                        .HasForeignKey("FkUserId")
                        .IsRequired()
                        .HasConstraintName("FK__Order__fkUserId__7C4F7684");

                    b.Navigation("FkBillingAddress");

                    b.Navigation("FkOrderStatus");

                    b.Navigation("FkShippingAddress");

                    b.Navigation("FkUser");
                });

            modelBuilder.Entity("Novella.EfModels.Product", b =>
                {
                    b.HasOne("Novella.EfModels.Category", "FkCategory")
                        .WithMany("Products")
                        .HasForeignKey("FkCategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__Product__fkCateg__6FE99F9F");

                    b.Navigation("FkCategory");
                });

            modelBuilder.Entity("Novella.EfModels.ProductCart", b =>
                {
                    b.HasOne("Novella.EfModels.Cart", "FkCart")
                        .WithMany("ProductCarts")
                        .HasForeignKey("FkCartId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductCa__fkCar__72C60C4A");

                    b.HasOne("Novella.EfModels.Product", "FkProduct")
                        .WithMany("ProductCarts")
                        .HasForeignKey("FkProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductCa__fkPro__73BA3083");

                    b.Navigation("FkCart");

                    b.Navigation("FkProduct");
                });

            modelBuilder.Entity("Novella.EfModels.ProductOrder", b =>
                {
                    b.HasOne("Novella.EfModels.Order", "FkOrder")
                        .WithMany("ProductOrders")
                        .HasForeignKey("FkOrderId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductOr__fkOrd__02084FDA");

                    b.HasOne("Novella.EfModels.Product", "FkProduct")
                        .WithMany("ProductOrders")
                        .HasForeignKey("FkProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductOr__fkPro__02FC7413");

                    b.Navigation("FkOrder");

                    b.Navigation("FkProduct");
                });

            modelBuilder.Entity("Novella.EfModels.Rating", b =>
                {
                    b.HasOne("Novella.EfModels.Product", "FkProduct")
                        .WithMany("Ratings")
                        .HasForeignKey("FkProductId")
                        .IsRequired()
                        .HasConstraintName("FK__Rating__fkProduc__778AC167");

                    b.HasOne("Novella.EfModels.UserAccount", "FkUser")
                        .WithMany("Ratings")
                        .HasForeignKey("FkUserId")
                        .IsRequired()
                        .HasConstraintName("FK__Rating__fkUserId__787EE5A0");

                    b.Navigation("FkProduct");

                    b.Navigation("FkUser");
                });

            modelBuilder.Entity("Novella.EfModels.Address", b =>
                {
                    b.Navigation("OrderFkBillingAddresses");

                    b.Navigation("OrderFkShippingAddresses");
                });

            modelBuilder.Entity("Novella.EfModels.Cart", b =>
                {
                    b.Navigation("ProductCarts");
                });

            modelBuilder.Entity("Novella.EfModels.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Novella.EfModels.Order", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("Novella.EfModels.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Novella.EfModels.Product", b =>
                {
                    b.Navigation("ImageStores");

                    b.Navigation("ProductCarts");

                    b.Navigation("ProductOrders");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Novella.EfModels.UserAccount", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}

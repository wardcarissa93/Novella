using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Novella.Migrations
{
    /// <inheritdoc />
    public partial class initalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    pkAddressId = table.Column<int>(type: "int", nullable: false),
                    addressLineOne = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    addressLineTwo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    city = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    province = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false),
                    postalCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Address__70806256AACC0CB1", x => x.pkAddressId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    pkCategoryId = table.Column<int>(type: "int", nullable: false),
                    categoryName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    categoryDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__9457381F4B965F2D", x => x.pkCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    pkOrderStatusId = table.Column<int>(type: "int", nullable: false),
                    statusName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    statusDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderSta__ABDB6887AE43DF56", x => x.pkOrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    pkUserId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    firstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    lastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    paypalAccount = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAcco__1790FCDFA3D042B2", x => x.pkUserId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    pkProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    productDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    quantityAvailable = table.Column<int>(type: "int", nullable: false),
                    fkCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__4492A4B549F021F0", x => x.pkProductId);
                    table.ForeignKey(
                        name: "FK__Product__fkCateg__6FE99F9F",
                        column: x => x.fkCategoryId,
                        principalTable: "Category",
                        principalColumn: "pkCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    pkCartId = table.Column<int>(type: "int", nullable: false),
                    fkUserId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__07D27F95AC63F6F0", x => x.pkCartId);
                    table.ForeignKey(
                        name: "FK__Cart__fkUserId__6C190EBB",
                        column: x => x.fkUserId,
                        principalTable: "UserAccount",
                        principalColumn: "pkUserId");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    pkOrderId = table.Column<int>(type: "int", nullable: false),
                    fkUserId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fkOrderStatusId = table.Column<int>(type: "int", nullable: false),
                    fkShippingAddressId = table.Column<int>(type: "int", nullable: false),
                    fkBillingAddressId = table.Column<int>(type: "int", nullable: false),
                    dateOrdered = table.Column<DateTime>(type: "datetime", nullable: false),
                    paypalTransactionId = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__C196130B2F15988C", x => x.pkOrderId);
                    table.ForeignKey(
                        name: "FK__Order__fkBilling__7F2BE32F",
                        column: x => x.fkBillingAddressId,
                        principalTable: "Address",
                        principalColumn: "pkAddressId");
                    table.ForeignKey(
                        name: "FK__Order__fkOrderSt__7D439ABD",
                        column: x => x.fkOrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "pkOrderStatusId");
                    table.ForeignKey(
                        name: "FK__Order__fkShippin__7E37BEF6",
                        column: x => x.fkShippingAddressId,
                        principalTable: "Address",
                        principalColumn: "pkAddressId");
                    table.ForeignKey(
                        name: "FK__Order__fkUserId__7C4F7684",
                        column: x => x.fkUserId,
                        principalTable: "UserAccount",
                        principalColumn: "pkUserId");
                });

            migrationBuilder.CreateTable(
                name: "ImageStore",
                columns: table => new
                {
                    pkImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    fkProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageStore", x => x.pkImageId);
                    table.ForeignKey(
                        name: "FK__ImageStor__fkPro__09746778",
                        column: x => x.fkProductId,
                        principalTable: "Product",
                        principalColumn: "pkProductId");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    pkRatingId = table.Column<int>(type: "int", nullable: false),
                    fkProductId = table.Column<int>(type: "int", nullable: false),
                    fkUserId = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    review = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ratingValue = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    dateRated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rating__38287FB2B9A9BCC4", x => x.pkRatingId);
                    table.ForeignKey(
                        name: "FK__Rating__fkProduc__778AC167",
                        column: x => x.fkProductId,
                        principalTable: "Product",
                        principalColumn: "pkProductId");
                    table.ForeignKey(
                        name: "FK__Rating__fkUserId__787EE5A0",
                        column: x => x.fkUserId,
                        principalTable: "UserAccount",
                        principalColumn: "pkUserId");
                });

            migrationBuilder.CreateTable(
                name: "ProductCart",
                columns: table => new
                {
                    pkProductCartId = table.Column<int>(type: "int", nullable: false),
                    fkCartId = table.Column<int>(type: "int", nullable: false),
                    fkProductId = table.Column<int>(type: "int", nullable: false),
                    quantityInCart = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductC__B08CDA1C902E01C7", x => x.pkProductCartId);
                    table.ForeignKey(
                        name: "FK__ProductCa__fkCar__72C60C4A",
                        column: x => x.fkCartId,
                        principalTable: "Cart",
                        principalColumn: "pkCartId");
                    table.ForeignKey(
                        name: "FK__ProductCa__fkPro__73BA3083",
                        column: x => x.fkProductId,
                        principalTable: "Product",
                        principalColumn: "pkProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    pkProductOrderId = table.Column<int>(type: "int", nullable: false),
                    fkOrderId = table.Column<int>(type: "int", nullable: false),
                    fkProductId = table.Column<int>(type: "int", nullable: false),
                    quantityInOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductO__17747A35F0FF20CC", x => x.pkProductOrderId);
                    table.ForeignKey(
                        name: "FK__ProductOr__fkOrd__02084FDA",
                        column: x => x.fkOrderId,
                        principalTable: "Order",
                        principalColumn: "pkOrderId");
                    table.ForeignKey(
                        name: "FK__ProductOr__fkPro__02FC7413",
                        column: x => x.fkProductId,
                        principalTable: "Product",
                        principalColumn: "pkProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_fkUserId",
                table: "Cart",
                column: "fkUserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Category__0007481DDDE4E047",
                table: "Category",
                column: "categoryDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Category__37077ABD15FE1CDE",
                table: "Category",
                column: "categoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageStore_fkProductId",
                table: "ImageStore",
                column: "fkProductId");

            migrationBuilder.CreateIndex(
                name: "UQ__ImageSto__431DED434E59CA40",
                table: "ImageStore",
                column: "fileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_fkBillingAddressId",
                table: "Order",
                column: "fkBillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_fkOrderStatusId",
                table: "Order",
                column: "fkOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_fkShippingAddressId",
                table: "Order",
                column: "fkShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_fkUserId",
                table: "Order",
                column: "fkUserId");

            migrationBuilder.CreateIndex(
                name: "UQ__OrderSta__6A50C212A8552E14",
                table: "OrderStatus",
                column: "statusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__OrderSta__B1D85D3F9C6D1F4A",
                table: "OrderStatus",
                column: "statusDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_fkCategoryId",
                table: "Product",
                column: "fkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCart_fkCartId",
                table: "ProductCart",
                column: "fkCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCart_fkProductId",
                table: "ProductCart",
                column: "fkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_fkOrderId",
                table: "ProductOrder",
                column: "fkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_fkProductId",
                table: "ProductOrder",
                column: "fkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_fkProductId",
                table: "Rating",
                column: "fkProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_fkUserId",
                table: "Rating",
                column: "fkUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageStore");

            migrationBuilder.DropTable(
                name: "ProductCart");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}

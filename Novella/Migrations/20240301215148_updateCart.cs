using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Novella.Migrations
{
    /// <inheritdoc />
    public partial class updateCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdminVM");

            migrationBuilder.DropTable(
                name: "ProductVM");

            migrationBuilder.AlterColumn<decimal>(
                name: "rating",
                table: "Rating",
                type: "decimal(2,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "pkProductId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.CreateIndex(
                name: "IX_ImageStore_fkProductId",
                table: "ImageStore",
                column: "fkProductId");

            migrationBuilder.CreateIndex(
                name: "UQ__ImageSto__431DED434E59CA40",
                table: "ImageStore",
                column: "fileName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageStore");

            migrationBuilder.AlterColumn<decimal>(
                name: "rating",
                table: "Rating",
                type: "decimal(2,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");

            migrationBuilder.AlterColumn<int>(
                name: "pkProductId",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "ProductAdminVM",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdminVM", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductVM",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVM", x => x.ProductId);
                });
        }
    }
}

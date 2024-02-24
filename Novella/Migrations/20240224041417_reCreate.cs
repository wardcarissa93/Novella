using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Novella.Migrations
{
    /// <inheritdoc />
    public partial class reCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductVM",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVM", x => x.ProductId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVM");

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantityInStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

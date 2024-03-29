using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Novella.Migrations
{
    /// <inheritdoc />
    public partial class ProductDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ImageStor__fkPro__09746778",
                table: "ImageStore");

            migrationBuilder.AddForeignKey(
                name: "FK__ImageStor__fkPro__09746778",
                table: "ImageStore",
                column: "fkProductId",
                principalTable: "Product",
                principalColumn: "pkProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ImageStor__fkPro__09746778",
                table: "ImageStore");

            migrationBuilder.AddForeignKey(
                name: "FK__ImageStor__fkPro__09746778",
                table: "ImageStore",
                column: "fkProductId",
                principalTable: "Product",
                principalColumn: "pkProductId");
        }
    }
}

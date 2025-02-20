using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSENA.Migrations.Product
{
    /// <inheritdoc />
    public partial class AddingProductModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductAddingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductNetworkPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductStockNumber = table.Column<int>(type: "int", nullable: false),
                    ProductDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

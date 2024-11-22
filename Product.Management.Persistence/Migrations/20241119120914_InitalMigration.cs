using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDbSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductWeight = table.Column<double>(type: "float", nullable: false),
                    Units = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDbSet", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductDbSet",
                columns: new[] { "Id", "Category", "DateCreated", "DateModified", "Description", "Name", "ProductPrice", "ProductWeight", "Units" },
                values: new object[] { 1, "Dairy Products", new DateTime(2024, 11, 19, 17, 39, 9, 243, DateTimeKind.Local).AddTicks(1428), new DateTime(2024, 11, 19, 17, 39, 9, 243, DateTimeKind.Local).AddTicks(1462), "Amul Diced Cheese Blend Mozzeralla Cheddar", "Cheese", 116m, 200.0, 20.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDbSet");
        }
    }
}

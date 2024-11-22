using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Management.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductMgmt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProductWeight = table.Column<double>(type: "float", nullable: false),
                    Units = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMgmt", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductMgmt",
                columns: new[] { "Id", "Category", "DateCreated", "DateModified", "Description", "Name", "ProductPrice", "ProductWeight", "Units" },
                values: new object[] { 1, "Dairy Products", new DateTime(2024, 11, 21, 10, 59, 36, 618, DateTimeKind.Local).AddTicks(988), new DateTime(2024, 11, 21, 10, 59, 36, 618, DateTimeKind.Local).AddTicks(1004), "Amul Diced Cheese Blend Mozzeralla Cheddar", "Cheese", 116m, 200.0, 20.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMgmt");
        }
    }
}

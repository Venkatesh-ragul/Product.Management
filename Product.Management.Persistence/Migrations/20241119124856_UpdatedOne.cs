using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductPrice",
                table: "ProductDbSet",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "ProductDbSet",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 19, 18, 18, 54, 737, DateTimeKind.Local).AddTicks(5841), new DateTime(2024, 11, 19, 18, 18, 54, 737, DateTimeKind.Local).AddTicks(5855) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ProductPrice",
                table: "ProductDbSet",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.UpdateData(
                table: "ProductDbSet",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 19, 17, 39, 9, 243, DateTimeKind.Local).AddTicks(1428), new DateTime(2024, 11, 19, 17, 39, 9, 243, DateTimeKind.Local).AddTicks(1462) });
        }
    }
}

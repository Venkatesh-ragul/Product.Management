using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductDbSet",
                table: "ProductDbSet");

            migrationBuilder.RenameTable(
                name: "ProductDbSet",
                newName: "ProductMgmt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMgmt",
                table: "ProductMgmt",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ProductMgmt",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 19, 18, 28, 43, 825, DateTimeKind.Local).AddTicks(9328), new DateTime(2024, 11, 19, 18, 28, 43, 825, DateTimeKind.Local).AddTicks(9344) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMgmt",
                table: "ProductMgmt");

            migrationBuilder.RenameTable(
                name: "ProductMgmt",
                newName: "ProductDbSet");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductDbSet",
                table: "ProductDbSet",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ProductDbSet",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 11, 19, 18, 18, 54, 737, DateTimeKind.Local).AddTicks(5841), new DateTime(2024, 11, 19, 18, 18, 54, 737, DateTimeKind.Local).AddTicks(5855) });
        }
    }
}

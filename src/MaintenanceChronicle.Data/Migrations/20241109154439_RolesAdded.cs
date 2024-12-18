using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaintenanceChronicle.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("078500e4-4917-4cb0-b6ef-aeef745bcca8"), "F5C8757B-9165-4DAA-9076-4CEBD2BD9416", "Technician", "TECHNICIAN" },
                    { new Guid("3cd34831-a874-47b3-9ab0-5afd29edb69e"), "8D8BEBDE-9E70-4AD6-A3E8-34EDD0F7E0CE", "Admin", "ADMIN" },
                    { new Guid("fcd43167-b480-47d9-8a84-7b52c06e1ebb"), "26AB60F1-DACF-4CF1-996E-DB6E36E36CA5", "GlobalAdmin", "GLOBALADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("078500e4-4917-4cb0-b6ef-aeef745bcca8"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3cd34831-a874-47b3-9ab0-5afd29edb69e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fcd43167-b480-47d9-8a84-7b52c06e1ebb"));
        }
    }
}

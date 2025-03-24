using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tiketix.Migrations
{
    /// <inheritdoc />
    public partial class RolesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77ed1443-ebb3-447a-b18a-bc5a14eb6613");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d948dd3f-82be-4be2-914d-5c8824cd2bf2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "443e81f9-4197-4f61-82cb-707393fafc0d", null, "Client", "CLIENT" },
                    { "5cc22af8-0044-4057-bc48-fea7369e0eec", null, "Administrator", "ADMINISTRATOR" },
                    { "9c25fab4-ed63-499c-9ed3-8c06ad410d01", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "443e81f9-4197-4f61-82cb-707393fafc0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cc22af8-0044-4057-bc48-fea7369e0eec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c25fab4-ed63-499c-9ed3-8c06ad410d01");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77ed1443-ebb3-447a-b18a-bc5a14eb6613", null, "Administrator", "ADMINISTRATOR" },
                    { "d948dd3f-82be-4be2-914d-5c8824cd2bf2", null, "Manager", "MANAGER" }
                });
        }
    }
}

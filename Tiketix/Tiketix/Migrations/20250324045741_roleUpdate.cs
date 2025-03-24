using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tiketix.Migrations
{
    /// <inheritdoc />
    public partial class roleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

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
                    { "2e6660f2-9c82-459b-b2e6-a7e723201457", null, "Manager", "MANAGER" },
                    { "dd2a1e80-79ee-4282-b5a3-f76a01b65e2e", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e6660f2-9c82-459b-b2e6-a7e723201457");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd2a1e80-79ee-4282-b5a3-f76a01b65e2e");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "443e81f9-4197-4f61-82cb-707393fafc0d", null, "Client", "CLIENT" },
                    { "5cc22af8-0044-4057-bc48-fea7369e0eec", null, "Administrator", "ADMINISTRATOR" },
                    { "9c25fab4-ed63-499c-9ed3-8c06ad410d01", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Email", "FirstName", "LastName", "Password", "Phone" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "oredebby@gmail.com", "Ore", "Debby", "diuwdj293", "0209595986" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "tobi@gmail.com", "Tobi", "Temi", "jdjkjf55", "090565695" }
                });
        }
    }
}

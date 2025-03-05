using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tiketix.Migrations
{
    /// <inheritdoc />
    public partial class addMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EventTime",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Email", "FirstName", "LastName", "Password", "Phone" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "oredebby@gmail.com", "Ore", "Debby", "diuwdj293", "0209595986" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "tobi@gmail.com", "Tobi", "Temi", "jdjkjf55", "090565695" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventDate", "EventDescription", "EventTime", "EventTitle", "Location", "OrganizerEmail", "Phone", "TicketsAvailable" },
                values: new object[,]
                {
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), new DateOnly(2025, 3, 25), "It is a meeting event for people in tech", "7pm", "IT_Solutions Seminar", "100 Brick Dr. Gwynn Oak, MD 21207", "solutions@gmail.com", "0106896565", 100 },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new DateOnly(2025, 3, 2), "Boost your career forward in 20 minutes", "7pm", "Career_Solutions Ltd", "583 Wall Dr. Gwynn Oak, MD 21207", "career@gmail.com", "01895684", 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EventTime",
                table: "Events",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

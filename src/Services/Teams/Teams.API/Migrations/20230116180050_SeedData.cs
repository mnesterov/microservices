using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "cities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Atlanta" },
                    { 2, "Boston" },
                    { 3, "Charlotte" },
                    { 4, "Chicago" },
                    { 5, "Cleveland" },
                    { 6, "Detroit" },
                    { 7, "Los Angeles" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "nbateams",
                columns: new[] { "id", "cityid", "name", "salarybasket" },
                values: new object[,]
                {
                    { 1, 1, "Hawks", 0.0 },
                    { 2, 2, "Celtics", 0.0 },
                    { 3, 3, "Hornets", 0.0 },
                    { 4, 4, "Bulls", 0.0 },
                    { 5, 5, "Cavaliers", 0.0 },
                    { 6, 6, "Pistons", 0.0 },
                    { 7, 7, "Clippers", 0.0 },
                    { 8, 7, "Lakers", 0.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 7);
        }
    }
}

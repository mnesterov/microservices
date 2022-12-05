using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamsService.Migrations
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
                columns: new[] { "id", "cityid", "name" },
                values: new object[,]
                {
                    { 1, 1, "Hawks" },
                    { 2, 2, "Celtics" },
                    { 3, 3, "Hornets" },
                    { 4, 4, "Bulls" },
                    { 5, 5, "Cavaliers" },
                    { 6, 6, "Pistons" },
                    { 7, 7, "Clippers" },
                    { 8, 7, "Lakers" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "players",
                columns: new[] { "id", "birthday", "firstname", "lastname", "nbateamid" },
                values: new object[,]
                {
                    { 1, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Michael", "Jordan", 4 },
                    { 2, new DateTime(1965, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Scottie", "Pippen", 4 },
                    { 3, new DateTime(1961, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Dennis", "Rodman", 4 },
                    { 4, new DateTime(1964, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Ron", "Harper", 4 },
                    { 5, new DateTime(1956, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Larry", "Bird", 2 },
                    { 6, new DateTime(1957, 12, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Kevin", "McHale", 2 },
                    { 7, new DateTime(1953, 8, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Robert", "Parish", 2 },
                    { 8, new DateTime(1960, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Dominique", "Wilkins", 1 },
                    { 9, new DateTime(1963, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Spud", "Webb", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "nbateams",
                keyColumn: "id",
                keyValue: 3);

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
                table: "players",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "players",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "cities",
                keyColumn: "id",
                keyValue: 3);

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
                keyValue: 4);

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
                keyValue: 4);
        }
    }
}

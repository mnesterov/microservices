using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Players.API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "players",
                columns: new[] { "id", "birthday", "firstname", "lastname", "nbateamid", "salary", "contractlength" },
                values: new object[,]
                {
                    { 1, new DateTime(1963, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael", "Jordan", null, 30000000.0, 1 },
                    { 2, new DateTime(1965, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scottie", "Pippen", null, 20000000.0, 1 },
                    { 3, new DateTime(1961, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dennis", "Rodman", null, 10000000.0, 1 },
                    { 4, new DateTime(1964, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ron", "Harper", null, 5000000.0, 1 },
                    { 5, new DateTime(1956, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Larry", "Bird", null, 25000000.0, 1 },
                    { 6, new DateTime(1957, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kevin", "McHale", null, 20000000.0, 1 },
                    { 7, new DateTime(1953, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert", "Parish", null, 10000000.0, 1 },
                    { 8, new DateTime(1960, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dominique", "Wilkins", null, 25000000.0, 1 },
                    { 9, new DateTime(1963, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spud", "Webb", null, 10000000.0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "players",
                keyColumn: "id",
                keyValue: 9);
        }
    }
}

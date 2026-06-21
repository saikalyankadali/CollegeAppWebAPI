using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class addtabledata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DOB", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "kalyan@gmail.com", "Kalyan", "9988776633" },
                    { 2, new DateTime(1998, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "niha@gmail.com", "Niha", "8988776633" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

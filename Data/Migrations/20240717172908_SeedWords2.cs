using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedWords2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1",
                column: "DateAdded",
                value: new DateOnly(2024, 7, 16));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1",
                column: "DateAdded",
                value: new DateOnly(2024, 7, 17));
        }
    }
}

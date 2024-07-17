using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedWords1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "DateAdded", "Definition", "ExampleSentences", "PartOfSpeech", "Pronunciation", "Value" },
                values: new object[] { "1", new DateOnly(2024, 7, 17), "The occurrence and development of events by chance in a happy or beneficial way.", "[\"Example 1\",\"Example 2\"]", "Noun", "/ˌserənˈdipədi/", "Serendipity" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}

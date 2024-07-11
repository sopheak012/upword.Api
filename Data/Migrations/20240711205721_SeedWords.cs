using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedWords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "DateAdded", "Definition", "ExampleSentence", "PartOfSpeech", "Pronunciation", "Value" },
                values: new object[,]
                {
                    { "1", new DateOnly(2024, 7, 11), "The occurrence and development of events by chance in a happy or beneficial way.", "The discovery of penicillin was a serendipity.", "Noun", "/ˌserənˈdipədē/", "Serendipity" },
                    { "2", new DateOnly(2024, 7, 11), "Lasting for a very short time.", "The beauty of cherry blossoms is ephemeral, lasting only a few days.", "Adjective", "/əˈfem(ə)rəl/", "Ephemeral" },
                    { "3", new DateOnly(2024, 7, 11), "Present, appearing, or found everywhere.", "In the modern world, smartphones have become ubiquitous.", "Adjective", "/yo͞oˈbikwədəs/", "Ubiquitous" },
                    { "4", new DateOnly(2024, 7, 11), "Fluent or persuasive in speaking or writing.", "Her eloquent speech moved the entire audience.", "Adjective", "/ˈeləkwənt/", "Eloquent" },
                    { "5", new DateOnly(2024, 7, 11), "Persistence in doing something despite difficulty or delay in achieving success.", "His perseverance in the face of adversity was truly inspiring.", "Noun", "/ˌpərsəˈvirəns/", "Perseverance" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "5");
        }
    }
}

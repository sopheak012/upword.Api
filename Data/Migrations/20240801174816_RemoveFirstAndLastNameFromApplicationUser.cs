using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFirstAndLastNameFromApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1",
                column: "ExampleSentences",
                value: "[\"Finding that old coin while cleaning the attic was a moment of serendipity.\",\"Her meeting with the famous author was pure serendipity during her vacation.\",\"The serendipity of the accidental discovery led to a major scientific breakthrough.\"]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1",
                column: "ExampleSentences",
                value: "[\"Example 1\",\"Example 2\"]");
        }
    }
}

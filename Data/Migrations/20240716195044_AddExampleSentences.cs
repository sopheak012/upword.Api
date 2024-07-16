using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExampleSentences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExampleSentence",
                table: "Words",
                newName: "ExampleSentences");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExampleSentences",
                table: "Words",
                newName: "ExampleSentence");
        }
    }
}

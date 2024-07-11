using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    PartOfSpeech = table.Column<string>(type: "TEXT", nullable: true),
                    Pronunciation = table.Column<string>(type: "TEXT", nullable: true),
                    ExampleSentence = table.Column<string>(type: "TEXT", nullable: true),
                    DateAdded = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}

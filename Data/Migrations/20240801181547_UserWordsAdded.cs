using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upword.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserWordsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    WordId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1",
                column: "ExampleSentences",
                value: "[\"She found her old friend by sheer serendipity.\",\"Their serendipity led to a fruitful collaboration.\"]");

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_UserId",
                table: "UserWords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_WordId",
                table: "UserWords",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWords");

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: "1",
                column: "ExampleSentences",
                value: "[\"Finding that old coin while cleaning the attic was a moment of serendipity.\",\"Her meeting with the famous author was pure serendipity during her vacation.\",\"The serendipity of the accidental discovery led to a major scientific breakthrough.\"]");
        }
    }
}

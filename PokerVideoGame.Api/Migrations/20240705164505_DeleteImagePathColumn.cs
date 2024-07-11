using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokerVideoGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteImagePathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Deck");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Deck",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PokerVideoGame.Api.Migrations
{
    /// <inheritdoc />
    public partial class removeCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountBalance = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prize = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameHistories_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    ImagePath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CardValue = table.Column<int>(type: "int", nullable: false),
                    CardSuit = table.Column<int>(type: "int", nullable: false),
                    GameHistoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.ImagePath);
                    table.ForeignKey(
                        name: "FK_Card_GameHistories_GameHistoryId",
                        column: x => x.GameHistoryId,
                        principalTable: "GameHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AccountBalance", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, 1000, null, "Mikolaj", null },
                    { 2, 1000, null, "Bartosz", null },
                    { 3, 1000, null, "Kamil", null },
                    { 4, 1000, null, "Korneliusz", null },
                    { 5, 1000, null, "Amadeusz", null },
                    { 6, 1000, null, "Mateusz", null },
                    { 7, 1000, null, "Szymon", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_GameHistoryId",
                table: "Card",
                column: "GameHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHistories_PlayerId",
                table: "GameHistories",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "GameHistories");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}

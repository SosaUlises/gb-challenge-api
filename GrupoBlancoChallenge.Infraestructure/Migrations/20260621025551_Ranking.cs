using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoBlancoChallenge.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Ranking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinalRating",
                table: "ranking_entries",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FinalRating",
                table: "game_sessions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalRating",
                table: "ranking_entries");

            migrationBuilder.DropColumn(
                name: "FinalRating",
                table: "game_sessions");
        }
    }
}

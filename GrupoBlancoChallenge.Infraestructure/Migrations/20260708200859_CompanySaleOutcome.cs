using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoBlancoChallenge.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanySaleOutcome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinishedAtMonth",
                table: "ranking_entries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WasCompanySold",
                table: "ranking_entries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FinishedAtMonth",
                table: "game_sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WasCompanySold",
                table: "game_sessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(
                """UPDATE "ranking_entries" SET "FinishedAtMonth" = 12;""");

            migrationBuilder.Sql(
                """UPDATE "game_sessions" SET "FinishedAtMonth" = 12 WHERE "IsFinished" = TRUE;""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedAtMonth",
                table: "ranking_entries");

            migrationBuilder.DropColumn(
                name: "WasCompanySold",
                table: "ranking_entries");

            migrationBuilder.DropColumn(
                name: "FinishedAtMonth",
                table: "game_sessions");

            migrationBuilder.DropColumn(
                name: "WasCompanySold",
                table: "game_sessions");
        }
    }
}

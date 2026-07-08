using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoBlancoChallenge.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class MonthlyGameSessionScenarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "scenarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Quarter",
                table: "scenarios",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("""
                UPDATE scenarios
                SET
                    "Month" = "Order",
                    "Quarter" = CASE
                        WHEN "Order" BETWEEN 1 AND 3 THEN 'Q1'
                        WHEN "Order" BETWEEN 4 AND 6 THEN 'Q2'
                        WHEN "Order" BETWEEN 7 AND 9 THEN 'Q3'
                        ELSE 'Q4'
                    END
                WHERE "Month" = 0 OR "Quarter" = '';
                """);

            migrationBuilder.CreateTable(
                name: "game_session_scenarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScenarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_session_scenarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_game_session_scenarios_game_sessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "game_sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_session_scenarios_scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "scenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_session_scenarios_GameSessionId_Month",
                table: "game_session_scenarios",
                columns: new[] { "GameSessionId", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_session_scenarios_GameSessionId_Position",
                table: "game_session_scenarios",
                columns: new[] { "GameSessionId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_session_scenarios_ScenarioId",
                table: "game_session_scenarios",
                column: "ScenarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_session_scenarios");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "scenarios");

            migrationBuilder.DropColumn(
                name: "Quarter",
                table: "scenarios");
        }
    }
}

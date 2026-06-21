using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoBlancoChallenge.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class GameDecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game_decisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScenarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    DecisionOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_decisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_game_decisions_decision_options_DecisionOptionId",
                        column: x => x.DecisionOptionId,
                        principalTable: "decision_options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_game_decisions_game_sessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "game_sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_game_decisions_scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "scenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_decisions_DecisionOptionId",
                table: "game_decisions",
                column: "DecisionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_game_decisions_GameSessionId",
                table: "game_decisions",
                column: "GameSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_game_decisions_ScenarioId",
                table: "game_decisions",
                column: "ScenarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_decisions");
        }
    }
}

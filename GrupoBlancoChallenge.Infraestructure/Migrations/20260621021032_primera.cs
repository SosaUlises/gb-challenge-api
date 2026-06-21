using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrupoBlancoChallenge.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class primera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game_sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Rentability = table.Column<int>(type: "integer", nullable: false),
                    Clients = table.Column<int>(type: "integer", nullable: false),
                    OrganizationalClimate = table.Column<int>(type: "integer", nullable: false),
                    BrandImage = table.Column<int>(type: "integer", nullable: false),
                    OperationalEfficiency = table.Column<int>(type: "integer", nullable: false),
                    CurrentScenarioOrder = table.Column<int>(type: "integer", nullable: false),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false),
                    FinalScore = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ranking_entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FinalScore = table.Column<int>(type: "integer", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranking_entries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "scenarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Topic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scenarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "decision_options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScenarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Consequence = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    RentabilityImpact = table.Column<int>(type: "integer", nullable: false),
                    ClientsImpact = table.Column<int>(type: "integer", nullable: false),
                    OrganizationalClimateImpact = table.Column<int>(type: "integer", nullable: false),
                    BrandImageImpact = table.Column<int>(type: "integer", nullable: false),
                    OperationalEfficiencyImpact = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_decision_options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_decision_options_scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "scenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_decision_options_ScenarioId",
                table: "decision_options",
                column: "ScenarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "decision_options");

            migrationBuilder.DropTable(
                name: "game_sessions");

            migrationBuilder.DropTable(
                name: "ranking_entries");

            migrationBuilder.DropTable(
                name: "scenarios");
        }
    }
}

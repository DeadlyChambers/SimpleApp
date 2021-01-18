using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SimpleAPI.DataAccess.Football
{
    public partial class firstFootball00000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:adminpack", ",,");

            migrationBuilder.CreateTable(
                name: "players",
                schema: "public",
                columns: table => new
                {
                    playerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    firstname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    lastname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    middlename = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    height = table.Column<short>(type: "smallint", nullable: false),
                    weight = table.Column<short>(type: "smallint", nullable: false),
                    college = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    positions = table.Column<string[]>(type: "text[]", nullable: true),
                    status = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    draftYear = table.Column<short>(type: "smallint", nullable: true),
                    dob = table.Column<DateTime>(type: "date", nullable: false),
                    playingStatus = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.playerid);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                schema: "public",
                columns: table => new
                {
                    positionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.positionid);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "public",
                columns: table => new
                {
                    teamid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    conference = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    division = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.teamid);
                });

            migrationBuilder.CreateTable(
                name: "games",
                schema: "public",
                columns: table => new
                {
                    gameid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    hometeamid = table.Column<int>(type: "integer", nullable: false),
                    awayteamid = table.Column<int>(type: "integer", nullable: false),
                    gamedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    homescore = table.Column<short>(type: "smallint", nullable: false),
                    awayscore = table.Column<short>(type: "smallint", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.gameid);
                    table.ForeignKey(
                        name: "fk_away_team",
                        column: x => x.awayteamid,
                        principalSchema: "public",
                        principalTable: "teams",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_home_team",
                        column: x => x.hometeamid,
                        principalSchema: "public",
                        principalTable: "teams",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stats",
                schema: "public",
                columns: table => new
                {
                    gameid = table.Column<int>(type: "integer", nullable: false),
                    teamid = table.Column<int>(type: "integer", nullable: false),
                    playerid = table.Column<int>(type: "integer", nullable: false),
                    passingattempts = table.Column<short>(type: "smallint", nullable: false),
                    passingcompletions = table.Column<short>(type: "smallint", nullable: false),
                    passingyards = table.Column<short>(type: "smallint", nullable: false),
                    interceptions = table.Column<short>(type: "smallint", nullable: false),
                    fumbles = table.Column<short>(type: "smallint", nullable: false),
                    receptions = table.Column<short>(type: "smallint", nullable: false),
                    targets = table.Column<short>(type: "smallint", nullable: false),
                    drops = table.Column<short>(type: "smallint", nullable: false),
                    receivingyards = table.Column<short>(type: "smallint", nullable: false),
                    receivingtds = table.Column<short>(type: "smallint", nullable: false),
                    rushingattempts = table.Column<short>(type: "smallint", nullable: false),
                    rushingyards = table.Column<short>(type: "smallint", nullable: false),
                    rushingtds = table.Column<short>(type: "smallint", nullable: false),
                    otheryards = table.Column<short>(type: "smallint", nullable: false),
                    othertds = table.Column<short>(type: "smallint", nullable: false),
                    twopointconversion = table.Column<short>(type: "smallint", nullable: false),
                    points = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    rztarget = table.Column<short>(type: "smallint", nullable: true),
                    rzrush = table.Column<short>(type: "smallint", nullable: true),
                    started = table.Column<bool>(type: "boolean", nullable: false),
                    snaps = table.Column<short>(type: "smallint", nullable: false),
                    passingtds = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("uq_game_team_player", x => new { x.gameid, x.playerid, x.teamid });
                    table.ForeignKey(
                        name: "fk_game_stats",
                        column: x => x.gameid,
                        principalSchema: "public",
                        principalTable: "games",
                        principalColumn: "gameid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_player_stats",
                        column: x => x.playerid,
                        principalSchema: "public",
                        principalTable: "players",
                        principalColumn: "playerid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_team_stats",
                        column: x => x.teamid,
                        principalSchema: "public",
                        principalTable: "teams",
                        principalColumn: "teamid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "positions",
                columns: new[] { "positionid", "name" },
                values: new object[,]
                {
                    { 1, "None" },
                    { 2, "QB" },
                    { 4, "WR" },
                    { 8, "RB" },
                    { 16, "TE" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_games_awayteamid",
                schema: "public",
                table: "games",
                column: "awayteamid");

            migrationBuilder.CreateIndex(
                name: "ix_games_hometeamid",
                schema: "public",
                table: "games",
                column: "hometeamid");

            migrationBuilder.CreateIndex(
                name: "ix_stats_playerid",
                schema: "public",
                table: "stats",
                column: "playerid");

            migrationBuilder.CreateIndex(
                name: "ix_stats_teamid",
                schema: "public",
                table: "stats",
                column: "teamid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "positions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "stats",
                schema: "public");

            migrationBuilder.DropTable(
                name: "games",
                schema: "public");

            migrationBuilder.DropTable(
                name: "players",
                schema: "public");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "public");
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SimpleAPI.DataAccess;

namespace SimpleAPI.DataAccess.Football
{
    [DbContext(typeof(FootballContext))]
    partial class FootballContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                //.HasPostgresExtension("adminpack")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:Collation", "English_United States.1252")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("SimpleAPI.DataAccess.Game", b =>
                {
                    b.Property<int>("Gameid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("gameid")
                        .UseIdentityAlwaysColumn();

                    b.Property<short>("Awayscore")
                        .HasColumnType("smallint")
                        .HasColumnName("awayscore");

                    b.Property<int>("Awayteamid")
                        .HasColumnType("integer")
                        .HasColumnName("awayteamid");

                    b.Property<DateTime>("Createddate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<DateTime>("Gamedate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("gamedate");

                    b.Property<short>("Homescore")
                        .HasColumnType("smallint")
                        .HasColumnName("homescore");

                    b.Property<int>("Hometeamid")
                        .HasColumnType("integer")
                        .HasColumnName("hometeamid");

                    b.Property<DateTime?>("Modifieddate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modifieddate");

                    b.HasKey("Gameid")
                        .HasName("pk_games");

                    b.HasIndex("Awayteamid")
                        .HasDatabaseName("ix_games_awayteamid");

                    b.HasIndex("Hometeamid")
                        .HasDatabaseName("ix_games_hometeamid");

                    b.ToTable("games", "public");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Player", b =>
                {
                    b.Property<int>("Playerid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("playerid")
                        .UseIdentityAlwaysColumn();

                    b.Property<string>("College")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("college");

                    b.Property<DateTime>("Createddate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createddate");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<short?>("DraftYear")
                        .HasColumnType("smallint")
                        .HasColumnName("draftYear");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("firstname");

                    b.Property<short>("Height")
                        .HasColumnType("smallint")
                        .HasColumnName("height");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("lastname");

                    b.Property<string>("Middlename")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("middlename");

                    b.Property<DateTime?>("Modifieddate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modifieddate");

                    b.Property<string>("PlayingStatus")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("playingStatus");

                    b.Property<string[]>("Positions")
                        .HasColumnType("text[]")
                        .HasColumnName("positions");

                    b.Property<string>("Status")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("status");

                    b.Property<short>("Weight")
                        .HasColumnType("smallint")
                        .HasColumnName("weight");

                    b.HasKey("Playerid")
                        .HasName("pk_players");

                    b.ToTable("players", "public");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("positionid")
                        .UseIdentityAlwaysColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.HasKey("PositionId")
                        .HasName("pk_positions");

                    b.ToTable("positions", "public");

                    b.HasData(
                        new
                        {
                            PositionId = 1,
                            Name = "None"
                        },
                        new
                        {
                            PositionId = 2,
                            Name = "QB"
                        },
                        new
                        {
                            PositionId = 4,
                            Name = "WR"
                        },
                        new
                        {
                            PositionId = 8,
                            Name = "RB"
                        },
                        new
                        {
                            PositionId = 16,
                            Name = "TE"
                        });
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Stat", b =>
                {
                    b.Property<int>("Gameid")
                        .HasColumnType("integer")
                        .HasColumnName("gameid");

                    b.Property<int>("Playerid")
                        .HasColumnType("integer")
                        .HasColumnName("playerid");

                    b.Property<int>("Teamid")
                        .HasColumnType("integer")
                        .HasColumnName("teamid");

                    b.Property<short>("Drops")
                        .HasColumnType("smallint")
                        .HasColumnName("drops");

                    b.Property<short>("Fumbles")
                        .HasColumnType("smallint")
                        .HasColumnName("fumbles");

                    b.Property<short>("Interceptions")
                        .HasColumnType("smallint")
                        .HasColumnName("interceptions");

                    b.Property<short>("Othertds")
                        .HasColumnType("smallint")
                        .HasColumnName("othertds");

                    b.Property<short>("Otheryards")
                        .HasColumnType("smallint")
                        .HasColumnName("otheryards");

                    b.Property<short>("Passingattempts")
                        .HasColumnType("smallint")
                        .HasColumnName("passingattempts");

                    b.Property<short>("Passingcompletions")
                        .HasColumnType("smallint")
                        .HasColumnName("passingcompletions");

                    b.Property<short>("Passingtds")
                        .HasColumnType("smallint")
                        .HasColumnName("passingtds");

                    b.Property<short>("Passingyards")
                        .HasColumnType("smallint")
                        .HasColumnName("passingyards");

                    b.Property<decimal>("Points")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("points");

                    b.Property<short>("Receivingtds")
                        .HasColumnType("smallint")
                        .HasColumnName("receivingtds");

                    b.Property<short>("Receivingyards")
                        .HasColumnType("smallint")
                        .HasColumnName("receivingyards");

                    b.Property<short>("Receptions")
                        .HasColumnType("smallint")
                        .HasColumnName("receptions");

                    b.Property<short>("Rushingattempts")
                        .HasColumnType("smallint")
                        .HasColumnName("rushingattempts");

                    b.Property<short>("Rushingtds")
                        .HasColumnType("smallint")
                        .HasColumnName("rushingtds");

                    b.Property<short>("Rushingyards")
                        .HasColumnType("smallint")
                        .HasColumnName("rushingyards");

                    b.Property<short?>("Rzrush")
                        .HasColumnType("smallint")
                        .HasColumnName("rzrush");

                    b.Property<short?>("Rztarget")
                        .HasColumnType("smallint")
                        .HasColumnName("rztarget");

                    b.Property<short>("Snaps")
                        .HasColumnType("smallint")
                        .HasColumnName("snaps");

                    b.Property<bool>("Started")
                        .HasColumnType("boolean")
                        .HasColumnName("started");

                    b.Property<short>("Targets")
                        .HasColumnType("smallint")
                        .HasColumnName("targets");

                    b.Property<short>("Twopointconversion")
                        .HasColumnType("smallint")
                        .HasColumnName("twopointconversion");

                    b.HasKey("Gameid", "Playerid", "Teamid")
                        .HasName("uq_game_team_player");

                    b.HasIndex("Playerid")
                        .HasDatabaseName("ix_stats_playerid");

                    b.HasIndex("Teamid")
                        .HasDatabaseName("ix_stats_teamid");

                    b.ToTable("stats", "public");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Team", b =>
                {
                    b.Property<int>("Teamid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("teamid")
                        .UseIdentityAlwaysColumn();

                    b.Property<string>("Conference")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("conference");

                    b.Property<DateTime>("Createddate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createddate");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("division");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("location");

                    b.Property<DateTime?>("Modifieddate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modifieddate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Teamid")
                        .HasName("pk_teams");

                    b.ToTable("teams", "public");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Game", b =>
                {
                    b.HasOne("SimpleAPI.DataAccess.Team", "Awayteam")
                        .WithMany("GameAwayteams")
                        .HasForeignKey("Awayteamid")
                        .HasConstraintName("fk_away_team")
                        .IsRequired();

                    b.HasOne("SimpleAPI.DataAccess.Team", "Hometeam")
                        .WithMany("GameHometeams")
                        .HasForeignKey("Hometeamid")
                        .HasConstraintName("fk_home_team")
                        .IsRequired();

                    b.Navigation("Awayteam");

                    b.Navigation("Hometeam");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Stat", b =>
                {
                    b.HasOne("SimpleAPI.DataAccess.Game", "Game")
                        .WithMany("Stats")
                        .HasForeignKey("Gameid")
                        .HasConstraintName("fk_game_stats")
                        .IsRequired();

                    b.HasOne("SimpleAPI.DataAccess.Player", "Player")
                        .WithMany("Stats")
                        .HasForeignKey("Playerid")
                        .HasConstraintName("fk_player_stats")
                        .IsRequired();

                    b.HasOne("SimpleAPI.DataAccess.Team", "Team")
                        .WithMany("Stats")
                        .HasForeignKey("Teamid")
                        .HasConstraintName("fk_team_stats")
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Game", b =>
                {
                    b.Navigation("Stats");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Player", b =>
                {
                    b.Navigation("Stats");
                });

            modelBuilder.Entity("SimpleAPI.DataAccess.Team", b =>
                {
                    b.Navigation("GameAwayteams");

                    b.Navigation("GameHometeams");

                    b.Navigation("Stats");
                });
#pragma warning restore 612, 618
        }
    }
}

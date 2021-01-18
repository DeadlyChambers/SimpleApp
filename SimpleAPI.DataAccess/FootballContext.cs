using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleAPI.Common.Enums;
using SimpleAPI.DataAccess.Extensions;

namespace SimpleAPI.DataAccess
{
    public partial class FootballContext : DbContext {
        public FootballContext(DbContextOptions<FootballContext> options)
        : base(options)
        {
            // this.Database.Migrate();
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Stat> Stats { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Position> Postitions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
       .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United States.1252");

            // Ensure the Postions are statically loaded, migth need to move to top
            var roles = Enum.GetValues<PlayerPosition>().Select(x => x.ToPositionEntity()).ToArray();
            modelBuilder.Entity<Position>(e => e.HasData(roles));
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("games", "public");

                entity.Property(e => e.Gameid)
                    .HasColumnName("gameid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Awayscore).HasColumnName("awayscore");

                entity.Property(e => e.Awayteamid).HasColumnName("awayteamid");

                entity.Property(e => e.Createddate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("createddate");

                entity.Property(e => e.Gamedate).HasColumnName("gamedate");

                entity.Property(e => e.Homescore).HasColumnName("homescore");

                entity.Property(e => e.Hometeamid).HasColumnName("hometeamid");

                entity.Property(e => e.Modifieddate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("modifieddate");

                entity.HasOne(d => d.Awayteam)
                    .WithMany(p => p.GameAwayteams)
                    .HasForeignKey(d => d.Awayteamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_away_team");

                entity.HasOne(d => d.Hometeam)
                    .WithMany(p => p.GameHometeams)
                    .HasForeignKey(d => d.Hometeamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_home_team");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players", "public");

                entity.Property(e => e.Playerid)
                    .HasColumnName("playerid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.College)
                    .HasMaxLength(250)
                    .HasColumnName("college");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.DraftYear).HasColumnName("draftYear");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstname");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lastname");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(30)
                    .HasColumnName("middlename");

                entity.Property(e => e.Modifieddate).HasColumnName("modifieddate");

                entity.Property(e => e.PlayingStatus)
                    .HasMaxLength(3)
                    .HasColumnName("playingStatus");

                entity.Property(e => e.Positions).HasColumnName("positions");

                entity.Property(e => e.Status)
                    .HasMaxLength(3)
                    .HasColumnName("status");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Stat>(entity =>
            {
                entity.HasKey(e => new { e.Gameid, e.Playerid, e.Teamid })
                    .HasName("uq_game_team_player");

                entity.ToTable("stats", "public");

                entity.Property(e => e.Gameid).HasColumnName("gameid");

                entity.Property(e => e.Playerid).HasColumnName("playerid");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.Drops).HasColumnName("drops");

                entity.Property(e => e.Fumbles).HasColumnName("fumbles");

                entity.Property(e => e.Interceptions).HasColumnName("interceptions");

                entity.Property(e => e.Othertds).HasColumnName("othertds");

                entity.Property(e => e.Otheryards).HasColumnName("otheryards");

                entity.Property(e => e.Passingattempts).HasColumnName("passingattempts");

                entity.Property(e => e.Passingcompletions).HasColumnName("passingcompletions");

                entity.Property(e => e.Passingtds).HasColumnName("passingtds");

                entity.Property(e => e.Passingyards).HasColumnName("passingyards");

                entity.Property(e => e.Points)
                    .HasPrecision(5, 2)
                    .HasColumnName("points");

                entity.Property(e => e.Receivingtds).HasColumnName("receivingtds");

                entity.Property(e => e.Receivingyards).HasColumnName("receivingyards");

                entity.Property(e => e.Receptions).HasColumnName("receptions");

                entity.Property(e => e.Rushingattempts).HasColumnName("rushingattempts");

                entity.Property(e => e.Rushingtds).HasColumnName("rushingtds");

                entity.Property(e => e.Rushingyards).HasColumnName("rushingyards");

                entity.Property(e => e.Rzrush).HasColumnName("rzrush");

                entity.Property(e => e.Rztarget).HasColumnName("rztarget");

                entity.Property(e => e.Snaps).HasColumnName("snaps");

                entity.Property(e => e.Started).HasColumnName("started");

                entity.Property(e => e.Targets).HasColumnName("targets");

                entity.Property(e => e.Twopointconversion).HasColumnName("twopointconversion");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.Gameid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_game_stats");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.Playerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_player_stats");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.Teamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_team_stats");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams", "public");

                entity.Property(e => e.Teamid)
                    .HasColumnName("teamid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Conference)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("conference");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("division");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.Modifieddate).HasColumnName("modifieddate");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("positions", "public");

                entity.Property(e => e.PositionId)
                    .HasColumnName("positionid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
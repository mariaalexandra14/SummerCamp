using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SummerCamp.DataModels.Models;

public partial class SummerCampDbContext : DbContext
{
    public SummerCampDbContext()
    {
    }

    public SummerCampDbContext(DbContextOptions<SummerCampDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coach> Coaches { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionMatch> CompetitionMatches { get; set; }

    public virtual DbSet<CompetitionTeam> CompetitionTeams { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Sponsor> Sponsors { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamSponsor> TeamSponsors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SummerCamp");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Coach__3214EC076E29A192");

            entity.ToTable("Coach");

            entity.HasIndex(e => e.TeamId, "IX_Coach_TeamId")
                .IsUnique()
                .HasFilter("([TeamId] IS NOT NULL)");

            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Coaches)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__Coach__CountryId__571DF1D5");

            entity.HasOne(d => d.Team).WithOne(p => p.Coach)
                .HasForeignKey<Coach>(d => d.TeamId)
                .HasConstraintName("FK__Coach__TeamId__59063A47");
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC079828E44A");

            entity.ToTable("Competition");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Sponsor).WithMany(p => p.Competitions)
                .HasForeignKey(d => d.SponsorId)
                .HasConstraintName("FK__Competiti__Spons__31EC6D26");
        });

        modelBuilder.Entity<CompetitionMatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC077DB27188");

            entity.ToTable("CompetitionMatch");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.CompetitionMatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__AwayT__4CA06362");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionMatches)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__Compe__4AB81AF0");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.CompetitionMatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__HomeT__4BAC3F29");
        });

        modelBuilder.Entity<CompetitionTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Competit__3214EC07DCA3CF63");

            entity.ToTable("CompetitionTeam");

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Competiti__Compe__34C8D9D1");

            entity.HasOne(d => d.Team).WithMany(p => p.CompetitionTeams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Competiti__TeamI__35BCFE0A");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07D790F21B");

            entity.ToTable("Country");

            entity.HasIndex(e => new { e.Name, e.Flag }, "UQ_Country_Name_Flag").IsUnique();

            entity.Property(e => e.Flag)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC076C1E8908");

            entity.ToTable("Player");

            entity.HasIndex(e => new { e.TeamId, e.ShirtNumber }, "UQ_Team_Name").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.Players)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__Player__CountryI__5812160E");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__Player__TeamId__2C3393D0");
        });

        modelBuilder.Entity<Sponsor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sponsor__3214EC07BE1060B8");

            entity.ToTable("Sponsor");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Team__3214EC07C5821870");

            entity.ToTable("Team");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NickName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TeamSponsor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeamSpon__3214EC07BE085CF2");

            entity.ToTable("TeamSponsor");

            entity.HasOne(d => d.Sponsor).WithMany(p => p.TeamSponsors)
                .HasForeignKey(d => d.SponsorId)
                .HasConstraintName("FK__TeamSpons__Spons__38996AB5");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamSponsors)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamSpons__TeamI__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

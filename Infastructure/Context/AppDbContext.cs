
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;
using System.Reflection;
using System;
using AppCore.Models;
using Infrastracture.Entities;

namespace Infrastracture.Context;

public class AppDbContext : DbContext
{
    public DbSet<CountryEntity> countries { get; set; }
    public DbSet<UniversityEntity> universities { get; set; }
    public DbSet<University_yearEntity> university_Years { get; set; }
    public DbSet<Ranking_systemEntity> ranking_Systems { get; set; }
    public DbSet<Ranking_criteriaEntity> ranking_Criterias { get; set; }
    public DbSet<University_ranking_yearEntity> university_rankings { get; set; }

    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Host=localhost;Username=postgres;Password=123;Database=University_ranking";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CountryEntity>();
        modelBuilder.Entity<UniversityEntity>();
        modelBuilder.Entity<Ranking_systemEntity>();
        modelBuilder.Entity<Ranking_criteriaEntity>();
        /*
        //relacja jeden-do-wielu między country a university
        modelBuilder.Entity<CountryEntity>()
            .HasMany<UniversityEntity>()
            .WithOne();

        modelBuilder.Entity<UniversityEntity>()
            .HasOne<CountryEntity>()
            .WithMany()
            .HasForeignKey(a => a.country_id);

        //relacja jeden-do-wielu między ranking_system a ranking cryteria

        modelBuilder.Entity<Ranking_systemEntity>()
            .HasMany<Ranking_criteriaEntity>() 
            .WithOne();

        modelBuilder.Entity<Ranking_criteriaEntity>()
            .HasOne<Ranking_systemEntity>()
            .WithMany()
            .HasForeignKey(a => a.ranking_system_id);

        //relacja jeden-do-wielu między university a university_year i ranking_year

        modelBuilder.Entity<UniversityEntity>()
            .HasMany<University_yearEntity>()
            .WithOne();

        modelBuilder.Entity<University_yearEntity>()
            .HasOne<UniversityEntity>()
            .WithMany()
            .HasForeignKey(a => a.university_id);

        modelBuilder.Entity<UniversityEntity>()
            .HasMany<University_ranking_yearEntity>()
            .WithOne();

        modelBuilder.Entity<University_ranking_yearEntity>()
            .HasOne<UniversityEntity>()
            .WithMany()
            .HasForeignKey(a=>a.university_id);

        //relacja jeden-do-wielu między ranking_criteria a University_ranking_year

        modelBuilder.Entity<Ranking_criteriaEntity>()
            .HasMany<University_ranking_yearEntity>()
            .WithOne();

        modelBuilder.Entity<University_ranking_yearEntity>()
            .HasOne<Ranking_criteriaEntity>()
            .WithMany()
            .HasForeignKey(a => a.ranking_criteria_id);*/

    }
}
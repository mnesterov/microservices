using Microsoft.EntityFrameworkCore;
using DataAccess.EntityFramework.Mappings;
using Domain.Models;

namespace DataAccess.EntityFramework;

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new CitiesMapping().Configure(modelBuilder.Entity<City>());
        new TeamsMapping().Configure(modelBuilder.Entity<Team>());
        new PlayersMapping().Configure(modelBuilder.Entity<Player>());
    }

    #region Private Methods

    [Obsolete("Use migrations to seed data.")]
    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City(1, "Atlanta"),
            new City(2, "Boston"),
            new City(3, "Charlotte"),
            new City(4, "Chicago"),
            new City(5, "Cleveland"),
            new City(6, "Detroit"),
            new City(7, "Los Angeles"));

        modelBuilder.Entity<Team>().HasData(
            new Team(1, "Hawks", 1),
            new Team(2, "Celtics", 2),
            new Team(3, "Hornets", 3),
            new Team(4, "Bulls", 4),
            new Team(5, "Cavaliers", 5),
            new Team(6, "Pistons", 6),
            new Team(7, "Clippers", 7),
            new Team(8, "Lakers", 7));

        modelBuilder.Entity<Player>().HasData(
            new Player(1, new DateTime(1963,2,17), "Michael", "Jordan", 4),
            new Player(2, new DateTime(1965,9,25), "Scottie", "Pippen", 4),
            new Player(3, new DateTime(1961,5,13), "Dennis", "Rodman", 4),
            new Player(4, new DateTime(1964,1,20), "Ron", "Harper", 4),
            new Player(5, new DateTime(1956,12,7), "Larry", "Bird", 2),
            new Player(6, new DateTime(1957,12,19), "Kevin", "McHale", 2),
            new Player(7, new DateTime(1953,8,30), "Robert", "Parish", 2),
            new Player(8, new DateTime(1960,1,12), "Dominique", "Wilkins", 1),
            new Player(9, new DateTime(1963,7,17), "Spud", "Webb", 1));        
    }

    #endregion
}

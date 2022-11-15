using Microsoft.EntityFrameworkCore;
using DataAccess.EntityFramework.Mappings;
using Domain.Models;

namespace DataAccess.EntityFramework;

internal class AppDbContext : DbContext 
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
}

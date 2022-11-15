using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace DataAccess.EntityFramework.Mappings;

internal class TeamsMapping
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("nbateams", "public");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.CityId).HasColumnName("cityid");

        builder
            .HasOne(e => e.City)
            .WithMany(e => e.Teams)
            .HasForeignKey(e => e.CityId)
            .HasPrincipalKey(e => e.Id);

        builder
            .HasMany(e => e.Players)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId)
            .HasPrincipalKey(e => e.Id);
    }
}

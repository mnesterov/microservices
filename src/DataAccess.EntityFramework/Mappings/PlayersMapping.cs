using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace DataAccess.EntityFramework.Mappings;

internal class PlayersMapping
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players", "public");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Birthday).HasColumnName("birthday");
        builder.Property(e => e.FirstName).HasColumnName("firstname");
        builder.Property(e => e.LastName).HasColumnName("lastname");
        builder.Property(e => e.TeamId).HasColumnName("nbateamid");

        builder
            .HasOne(e => e.Team)
            .WithMany(e => e.Players)
            .HasForeignKey(e => e.TeamId)
            .HasPrincipalKey(e => e.Id);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Models.TeamAggregate;

namespace Teams.Infrastructure.EntityTypeConfigurations.Domain;

internal class TeamEntityTypeConfiguration
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("nbateams", "public");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").UseIdentityByDefaultColumn();
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.CityId).HasColumnName("cityid");
        builder.Property(e => e.SalaryBasket).HasColumnName("salarybasket");

        builder
            .HasOne(e => e.City)
            .WithMany()
            .HasForeignKey(e => e.CityId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(e => e.CityId, "nbateams_cityid_idx");
    }
}

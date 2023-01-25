using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Players.Domain.Models.PlayerAggregate;

namespace Players.Infrastructure.EntityTypeConfigurations.Domain
{
    public class PlayerTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");
            builder.HasKey(e => e.Id).HasName("pk_players");
            builder.Property(e => e.Id).HasColumnName("id").UseHiLo("playersseq");
            builder.Property(e => e.Birthday).HasColumnName("birthday");
            builder.Property(e => e.FirstName).HasColumnName("firstname");
            builder.Property(e => e.LastName).HasColumnName("lastname");
            builder.Property(e => e.TeamId).HasField("_teamId").HasColumnName("nbateamid");

            builder.OwnsOne(e => e.SalaryInfo, c =>
            {
                c.Property(c => c.ContractAnnualSalary).HasColumnName("salary");
                c.Property(c => c.ContractLength).HasColumnName("contractlength");
            });

            builder.HasIndex(e => e.TeamId, "players_nbateamid_idx");

//            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<Player> builder)
        {
            SeedPlayer(builder, 1, new DateTime(1963, 2, 17), "Michael", "Jordan", 30000000);
            SeedPlayer(builder, 2, new DateTime(1965, 9, 25), "Scottie", "Pippen", 20000000);
            SeedPlayer(builder, 3, new DateTime(1961, 5, 13), "Dennis", "Rodman", 10000000);
            SeedPlayer(builder, 4, new DateTime(1964, 1, 20), "Ron", "Harper", 5000000);
            SeedPlayer(builder, 5, new DateTime(1956, 12, 7), "Larry", "Bird", 25000000);
            SeedPlayer(builder, 6, new DateTime(1957, 12, 19), "Kevin", "McHale", 20000000);
            SeedPlayer(builder, 7, new DateTime(1953, 8, 30), "Robert", "Parish", 10000000);
            SeedPlayer(builder, 8, new DateTime(1960, 1, 12), "Dominique", "Wilkins", 25000000);
            SeedPlayer(builder, 9, new DateTime(1963, 7, 17), "Spud", "Webb", 10000000);
        }

        private static void SeedPlayer(EntityTypeBuilder<Player> builder,
            int id, DateTime birthday, string firstName, string lastName, double salary
            )
        {
            builder.HasData(new Player(id, birthday, firstName, lastName, salary));
            builder.OwnsOne(e => e.SalaryInfo).HasData(new
            {
                PlayerId = id,
                ContractAnnualSalary = salary,
                ContractLength = 1
            });
        }
    }
}
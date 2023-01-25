using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Players.Application.Commands.IdentifiedCommand.Request;

namespace Players.Infrastructure.EntityTypeConfigurations
{
    public class IdentifiedCommandRequestTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<IdentifiedCommandRequest> builder)
        {
            builder.ToTable("requests");
            builder.HasKey(e => e.Id).HasName("pk_requests");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Created).HasColumnName("created");
        }
    }
}
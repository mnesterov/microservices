using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Infrastructure.IntegrationEvents.EventLog.Models;

namespace Teams.Infrastructure.EntityTypeConfigurations
{
    public class IntegrationEventLogTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<IntegrationEventLog> builder)
        {
            builder.ToTable("eventlog", "public");
            builder.HasKey(e => e.EventId).HasName("pk_eventlog");
            builder.Property(e => e.EventId).HasColumnName("id");
            builder.Property(e => e.TransactionId).HasColumnName("transactionId");
            builder.Property(e => e.EventType).HasColumnName("eventType");
            builder.Property(e => e.Created).HasColumnName("created");
            builder.Property(e => e.Content).HasColumnName("content");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.SendAttempts).HasColumnName("sendAttempts");
        }
    }
}

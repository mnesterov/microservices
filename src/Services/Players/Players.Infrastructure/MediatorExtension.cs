using MediatR;
using Players.Domain.Common;

namespace Players.Infrastructure
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, AppDbContext ctx)
        {
            var entities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any()).ToList();

            var domainEvents = entities.SelectMany(e => e.Entity.DomainEvents).ToList();

            entities.ForEach(e => e.Entity.ClearDomainEvents());

            foreach (var e in domainEvents)
            {
                await mediator.Publish(e);
            }
        }
    }
}

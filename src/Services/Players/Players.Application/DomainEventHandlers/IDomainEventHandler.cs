using MediatR;
using Players.Domain.Events;

namespace Players.Application.DomainEventHandlers
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
    }
}

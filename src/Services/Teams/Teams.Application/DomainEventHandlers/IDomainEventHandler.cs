using MediatR;
using Teams.Domain.Events;

namespace Teams.Application.DomainEventHandlers
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
    }
}

using MassTransit;
using MediatR;
using Players.Application.Commands;
using Players.Application.Commands.IdentifiedCommand;
using Players.Application.IntegrationEvents.Events;

namespace Players.Application.IntegrationEvents.Consumers;

public class CancelTradeIntegrationEventConsumer : IConsumer<CancelTradeIntegrationEvent>
{
    private readonly IMediator _mediator;

    public CancelTradeIntegrationEventConsumer()
    {
    }

    public CancelTradeIntegrationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CancelTradeIntegrationEvent> context)
    {
        var @event = context.Message;

        var command = new CancelTradeCommand(
            context.Message.PlayerId,
            context.Message.OldTeamId);

        var identifiedCommand = new IdentifiedCommand<CancelTradeCommand, bool>(command, @event.EventId);

        await _mediator.Send(identifiedCommand);
    }
}
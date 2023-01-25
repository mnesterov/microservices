using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Teams.Application.Commands;
using Teams.Application.Commands.IdentifiedCommand;
using Teams.Application.IntegrationEvents.Events;

namespace Teams.Application.IntegrationEvents.Consumers;

public class TradePlayerIntegrationEventConsumer : IConsumer<TradePlayerIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<TradePlayerIntegrationEventConsumer> _logger;

    public TradePlayerIntegrationEventConsumer()
    {
    }

    public TradePlayerIntegrationEventConsumer(
        IMediator mediator,
        ILogger<TradePlayerIntegrationEventConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TradePlayerIntegrationEvent> context)
    {
        var @event = context.Message;

        var command = new TradePlayerCommand(
            @event.OldTeamId,
            @event.NewTeamId,
            @event.PlayerId,
            @event.PlayerContractAnnualSalary);

        var identifiedCommand = new IdentifiedCommand<TradePlayerCommand, bool>(command, @event.EventId);

        await _mediator.Send(identifiedCommand);
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using Teams.Application.Commands.IdentifiedCommand;
using Teams.Application.Commands.IdentifiedCommand.Request;
using Teams.Application.Mappers;
using Teams.Domain.Services;

namespace Teams.Application.Commands;

public class TradePlayerCommandHandler : ICommandHandler<TradePlayerCommand, bool>
{
    private ITeamsMapper _mapper;
    private readonly ITradeService _tradeService;

    public TradePlayerCommandHandler(ITeamsMapper mapper, ITradeService tradeService)
    {
        _mapper = mapper;
        _tradeService = tradeService;
    }

    public async Task<bool> Handle(TradePlayerCommand c, CancellationToken cancellationToken)
    {
        var tradeData = _mapper.Map<TradeData>(c);
        await _tradeService.ProcessTrade(tradeData, cancellationToken);
        return true;
    }
}

public class TradePlayerIdentifiedCommandHandler : IdentifiedCommandHandler<TradePlayerCommand, bool>
{
    public TradePlayerIdentifiedCommandHandler(
        IIdentifiedCommandRequestService identifiedCommandRequestService,
        ILogger<IdentifiedCommandHandler<TradePlayerCommand, bool>> logger,
        IMediator mediator)
        : base(identifiedCommandRequestService, logger, mediator)
    {
    }
}
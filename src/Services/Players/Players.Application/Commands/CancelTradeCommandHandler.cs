using MediatR;
using Microsoft.Extensions.Logging;
using Players.Application.Commands.IdentifiedCommand;
using Players.Application.Commands.IdentifiedCommand.Request;
using Players.Domain.Models.PlayerAggregate;

namespace Players.Application.Commands;

public class CancelTradeCommandHandler : ICommandHandler<CancelTradeCommand, bool>
{
    private readonly IPlayersRepository _playersRepository;
    private readonly ILogger<CancelTradeCommandHandler> _logger;

    public CancelTradeCommandHandler(
        IPlayersRepository playersRepository,
        ILogger<CancelTradeCommandHandler> logger)
    {
        _playersRepository = playersRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(CancelTradeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"--> Return player {request.PlayerId} back to {request.OldTeamId}");

        var player = await _playersRepository.GetPlayerAsync(request.PlayerId);
        player.AssignBackToOldTeam(request.OldTeamId);

        _playersRepository.UpdatePlayer(player);

        return await _playersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

public class CancelTradeIdentifiedCommandHandler : IdentifiedCommandHandler<CancelTradeCommand, bool>
{
    public CancelTradeIdentifiedCommandHandler(
        IIdentifiedCommandRequestService identifiedCommandRequestService,
        ILogger<IdentifiedCommandHandler<CancelTradeCommand, bool>> logger,
        IMediator mediator)
        : base(identifiedCommandRequestService, logger, mediator)
    {
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using Players.Application.Commands.IdentifiedCommand;
using Players.Application.Commands.IdentifiedCommand.Request;
using Players.Application.Validation;
using Players.Domain.Models.PlayerAggregate;

namespace Players.Application.Commands;

public class ChangePlayerTeamCommandHandler : ICommandHandler<ChangePlayerTeamCommand, bool>
{
    private readonly IPlayersRepository _playersRepository;
    private readonly ILogger<ChangePlayerTeamCommandHandler> _logger;

    public ChangePlayerTeamCommandHandler(
        IPlayersRepository playersRepository,
        ILogger<ChangePlayerTeamCommandHandler> logger)
    {
        _playersRepository = playersRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(ChangePlayerTeamCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"--> Player {request.PlayerId} assigning to {request.TeamId}");

        var player = await _playersRepository.GetPlayerAsync(request.PlayerId);
        Ensure.IsFound(player);

        player.AssignToNewTeam(request.TeamId);

        _playersRepository.UpdatePlayer(player);

        return await _playersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

public class ChangePlayerTeamIdentifiedCommandHandler : IdentifiedCommandHandler<ChangePlayerTeamCommand, bool>
{
    public ChangePlayerTeamIdentifiedCommandHandler(
        IIdentifiedCommandRequestService identifiedCommandRequestService,
        ILogger<IdentifiedCommandHandler<ChangePlayerTeamCommand, bool>> logger,
        IMediator mediator) 
        : base(identifiedCommandRequestService, logger, mediator)
    {
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using Players.Application.Commands.IdentifiedCommand;
using Players.Application.Commands.IdentifiedCommand.Request;
using Players.Application.Dtos;
using Players.Application.Mappers;
using Players.Domain.Models.PlayerAggregate;

namespace Players.Application.Commands;

public class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand, PlayerDto>
{
    private readonly IPlayersRepository _playersRepository;
    private readonly ILogger<CreatePlayerCommandHandler> _logger;
    private readonly IPlayersMapper _mapper;

    public CreatePlayerCommandHandler(
        IPlayersRepository playersRepository,
        ILogger<CreatePlayerCommandHandler> logger,
        IPlayersMapper mapper)
    {
        _playersRepository = playersRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"--> Creating Player {request.FirstName} {request.LastName}");

        var createData = _mapper.Map<Player.CreateData>(request);
        createData.Birthday = DateTime.SpecifyKind(createData.Birthday, DateTimeKind.Utc);

        var player = new Player(createData);
        var result = _playersRepository.AddPlayer(player);

        await _playersRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var dto = _mapper.Map<PlayerDto>(result);
        return dto;
    }
}

public class CreatePlayerIdentifiedCommandHandler : IdentifiedCommandHandler<CreatePlayerCommand, PlayerDto>
{
    public CreatePlayerIdentifiedCommandHandler(
        IIdentifiedCommandRequestService identifiedCommandRequestService,
        ILogger<IdentifiedCommandHandler<CreatePlayerCommand, PlayerDto>> logger,
        IMediator mediator)
        : base(identifiedCommandRequestService, logger, mediator)
    {
    }
}

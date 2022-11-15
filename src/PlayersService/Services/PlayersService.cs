using Domain.Repositories;
using PlayersService.Mappers;
using Dtos;

namespace PlayersService.Services;

public class PlayersService : IPlayersService
{
    private readonly IPlayersRepository _playersRepository;
    private readonly IPlayersMapper _mapper;

    public PlayersService(IPlayersRepository playersRepository, IPlayersMapper mapper)
    {
        _playersRepository = playersRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<PlayerDto>> GetPlayersAsync(int? teamId) 
    {
        if (teamId.HasValue)
        {
            return await GetPlayersByTeamAsync(teamId.Value);
        }

        var players = await _playersRepository.GetPlayersAsync();
        var dtos = players.Select(t => _mapper.Map<PlayerDto>(t)).ToList();
        return dtos;
    }

    #region Private Methods

    private async Task<ICollection<PlayerDto>> GetPlayersByTeamAsync(int teamId) 
    {
        var players = await _playersRepository.GetPlayersByTeamAsync(teamId);
        var dtos = players.Select(t => _mapper.Map<PlayerDto>(t)).ToList();
        return dtos;
    }

    #endregion
} 

using Domain.Repositories;
using PlayersService.Mappers;
using Dtos;
using Domain.Models;
using System.Transactions;

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

    public async Task<PlayerDto> CreatePlayerAsync(PlayerDto.CreateData data)
    {
        var createData = _mapper.Map<Player.CreateData>(data);
        var player = await _playersRepository.CreatePlayerAsync(createData);
        
        var dto = _mapper.Map<PlayerDto>(player);
        return dto;
    }

    public async Task UpdatePlayersTeamsAsync(TeamRosterDto.UpdateData data)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var newPlayers = data.PlayersIds;
            var currentPlayers = (await _playersRepository.GetPlayersByTeamAsync(data.TeamId)).Select(p => p.Id).ToList();
            
            var removed = currentPlayers.Except(newPlayers).ToList();
            var added = newPlayers.Except(currentPlayers).ToList();
            
            await _playersRepository.UpdateTeamForPlayersAsync(removed, null);
            await _playersRepository.UpdateTeamForPlayersAsync(added, data.TeamId);

            transaction.Complete();  
        }
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

using Dtos;
using TeamsService.Mappers;
using Infrastructure.Validation;
using Domain.Repositories;

namespace TeamsService.Services;

public class TeamsService : ITeamsService
{
    private readonly ITeamsRepository _teamsRepository;
    private readonly ITeamsMapper _mapper;

    public TeamsService(ITeamsRepository teamsRepository, ITeamsMapper mapper)
    {
        _teamsRepository = teamsRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<TeamDto>> GetTeamsAsync() 
    {
        var teams = await _teamsRepository.GetTeamsAsync();
        var dtos = teams.Select(t => _mapper.Map<TeamDto>(t)).ToList();
        return dtos;
    }

    public async Task<TeamDto> GetTeamAsync(int id)
    {
        var team = await _teamsRepository.GetTeamAsync(id);
        Ensure.IsFound(team);

        var dto = _mapper.Map<TeamDto>(team);
        return dto;
    }
} 

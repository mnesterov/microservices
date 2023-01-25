using Teams.Application.Mappers;
using Teams.Domain.Repositories;
using Teams.Dtos;

namespace Teams.Application.Queries
{
    public class TeamsQueryService : ITeamsQueryService
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly ITeamsMapper _mapper;

        public TeamsQueryService(ITeamsRepository teamsRepository, ITeamsMapper mapper)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
        }

        public async Task<TeamDto> GetTeamAsync(int id)
        {
            var team = await _teamsRepository.GetTeamAsync(id);
            var dto = _mapper.Map<TeamDto>(team);
            return dto;
        }

        public async Task<ICollection<TeamDto>> GetTeamsAsync()
        {
            var teams = await _teamsRepository.GetTeamsAsync();
            var list = teams.Select(t => _mapper.Map<TeamDto>(t)).ToList();
            return list;
        }
    }
}

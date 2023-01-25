using Players.Application.Dtos;
using Players.Application.Mappers;
using Players.Domain.Models.PlayerAggregate;

namespace Players.Application.Queries
{
    public class PlayersQueryService : IPlayersQueryService
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IPlayersMapper _mapper;

        public PlayersQueryService(
            IPlayersRepository playersRepository,
            IPlayersMapper mapper)
        {
            _playersRepository = playersRepository;
            _mapper = mapper;
        }

        public async Task<PlayerDto> GetPlayerAsync(int id)
        {
            var player = await _playersRepository.GetPlayerAsync(id);
            var dto = _mapper.Map<PlayerDto>(player);
            return dto;
        }

        public async Task<ICollection<PlayerDto>> GetPlayersAsync(int? teamId)
        {
            var players = teamId.HasValue 
                ? await _playersRepository.GetPlayersByTeamAsync(teamId.Value)
                : await _playersRepository.GetPlayersAsync();

            var list = players.Select(p => _mapper.Map<PlayerDto>(p)).ToList();
            return list;
        }
    }
}

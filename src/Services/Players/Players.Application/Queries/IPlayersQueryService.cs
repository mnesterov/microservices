using Players.Application.Dtos;

namespace Players.Application.Queries
{
    public interface IPlayersQueryService
    {
        Task<ICollection<PlayerDto>> GetPlayersAsync(int? teamId);
        Task<PlayerDto> GetPlayerAsync(int id);
    }
}

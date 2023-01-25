using Microsoft.AspNetCore.Mvc;
using Players.Application.Commands;
using WebHttpAggregator.Dtos;

namespace WebHttpAggregator.Services.Players
{
    public interface IPlayersService
    {
        Task<ActionResult<ICollection<PlayerDto>>> GetPlayersAsync(int? teamId);
        Task<ActionResult<PlayerDto>> GetPlayerAsync(int id);
        Task<ActionResult<PlayerDto>> CreatePlayerAsync(PlayerCreateRequest playerCreateRequest);
        Task<ActionResult> SetPlayerTeamAsync(int playerId, int teamId);
    }
}

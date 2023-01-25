using Microsoft.AspNetCore.Mvc;
using WebHttpAggregator.Dtos;

namespace WebHttpAggregator.Services
{
    public interface ITeamsService
    {
        Task<ActionResult<ICollection<TeamDto>>> GetTeamsAsync();
        Task<ActionResult<TeamDto>> GetTeamAsync(int id);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebHttpAggregator.Configs;
using WebHttpAggregator.Dtos;

namespace WebHttpAggregator.Services.Teams
{
    public class TeamsService : ITeamsService
    {
        private readonly TeamsApiEndpoints _teamsApiEndpoints;
        private readonly IHttpApiClient _httpApiClient;

        public TeamsService(
            IOptions<ApiEndpointsConfig> apiEndpoints,
            IHttpApiClient httpApiClient)
        {
            _teamsApiEndpoints = new TeamsApiEndpoints(apiEndpoints.Value.Teams);
            _httpApiClient = httpApiClient;
        }

        public Task<ActionResult<TeamDto>> GetTeamAsync(int id)
        {
            var uri = _teamsApiEndpoints.GetTeam(id);
            return _httpApiClient.GetAsync<TeamDto>(uri.ToString());
        }

        public Task<ActionResult<ICollection<TeamDto>>> GetTeamsAsync()
        {
            var uri = _teamsApiEndpoints.GetTeams();
            return _httpApiClient.GetAsync<ICollection<TeamDto>>(uri.ToString());
        }
    }
}

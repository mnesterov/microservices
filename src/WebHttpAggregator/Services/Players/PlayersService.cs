using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Players.Application.Commands;
using WebHttpAggregator.Configs;
using WebHttpAggregator.Dtos;

namespace WebHttpAggregator.Services.Players
{
    public class PlayersService : IPlayersService
    {
        private readonly PlayersApiEndpoints _playersApiEndpoints;
        private readonly IHttpApiClient _httpApiClient;

        public PlayersService(
            IOptions<ApiEndpointsConfig> apiEndpoints,
            IHttpApiClient httpApiClient)
        {
            _playersApiEndpoints = new PlayersApiEndpoints(apiEndpoints.Value.Players);
            _httpApiClient = httpApiClient;
        }

        public async Task<ActionResult<PlayerDto>> CreatePlayerAsync(PlayerCreateRequest playerCreateRequest)
        {
            var uri = _playersApiEndpoints.CreatePlayer();
            var result = await _httpApiClient.PostAsync<PlayerDto, PlayerCreateRequest>(uri.ToString(), playerCreateRequest);
            return result;
        }

        public async Task<ActionResult> SetPlayerTeamAsync(int playerId, int teamId)
        {
            var uri = _playersApiEndpoints.SetPlayerTeam(playerId);
            var result = await _httpApiClient.PutAsync(uri.ToString(), teamId);
            return result;
        }

        public Task<ActionResult<PlayerDto>> GetPlayerAsync(int id)
        {
            var uri = _playersApiEndpoints.GetPlayer(id);
            var result = _httpApiClient.GetAsync<PlayerDto>(uri.ToString());
            return result;
        }

        public Task<ActionResult<ICollection<PlayerDto>>> GetPlayersAsync(int? teamId)
        {
            var uri = _playersApiEndpoints.GetPlayers(teamId);
            var result = _httpApiClient.GetAsync<ICollection<PlayerDto>>(uri.ToString());
            return result;
        }
    }
}

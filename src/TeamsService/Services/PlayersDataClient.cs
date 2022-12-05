using Domain.Repositories;
using Dtos;
using Infrastructure.Serialization.Json;
using Infrastructure.Validation;

namespace TeamsService.Services;

public class PlayersDataClient : IPlayersDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ITeamsRepository _teamsRepository;
    private readonly ILogger<PlayersDataClient> _logger;

    public PlayersDataClient(HttpClient httpClient, 
        IConfiguration configuration, 
        ITeamsRepository teamsRepository,
        ILogger<PlayersDataClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _teamsRepository = teamsRepository;
        _logger = logger;
    }

    public async Task<ICollection<PlayerDto>> GetTeamPlayersAsync(int teamId) 
    {
        try
        {
            var team = await _teamsRepository.GetTeamAsync(teamId);
            Ensure.IsFound(team);

            var url = $"{_configuration["PlayersApiEndpoint"]}/api/players?teamid={teamId}"; 
            string content;
            
            using (var response = await _httpClient.GetAsync(url))
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var players = JsonSerializationHelper.Deserialize<ICollection<PlayerDto>>(content);

            return players;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"--> Exception is thrown when attempting to access /players from Teams controller: {e}");
            throw;
        }
    }
} 

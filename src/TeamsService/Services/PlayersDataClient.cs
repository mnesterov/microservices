using System.Text.Json;
using Domain.Repositories;
using Dtos;
using Infrastructure.Exceptions;
using Infrastructure.Formatters.Json;
using Infrastructure.Validation;

namespace TeamsService.Services;

public class PlayersDataClient : IPlayersDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ITeamsRepository _teamsRepository;

    public PlayersDataClient(HttpClient httpClient, IConfiguration configuration, ITeamsRepository teamsRepository)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _teamsRepository = teamsRepository;
    }

    public async Task<ICollection<PlayerDto>> GetTeamPlayersAsync(int teamId) 
    {
        try
        {
            var team = await _teamsRepository.GetTeamAsync(teamId);
            Ensure.IsFound(team);

            var url = $"{_configuration["PlayersApiEndpoint"]}/api/players?teamid={teamId}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions{ PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance };
            var players = JsonSerializer.Deserialize<ICollection<PlayerDto>>(content, options);

            return players;
        }
        catch
        {
            //simplified exception handling for testing purposes
            throw new NotFoundException();
        }
    }
} 

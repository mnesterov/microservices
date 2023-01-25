namespace WebHttpAggregator.Services.Players
{
    public class PlayersApiEndpoints
    {
        private Uri _playersBaseUri;

        public PlayersApiEndpoints(string playersBaseUrl)
        {
            _playersBaseUri = new Uri(playersBaseUrl);
        }

        public string GetPlayers(int? teamId) => new Uri(_playersBaseUri, $"/api/v1/players?teamid={teamId}").ToString();
        public string GetPlayer(int playerId) => new Uri(_playersBaseUri, $"/api/v1/players/{playerId}").ToString();
        public string CreatePlayer() => new Uri(_playersBaseUri, $"/api/v1/players").ToString();
        public string SetPlayerTeam(int playerId) => new Uri(_playersBaseUri, $"/api/v1/players/{playerId}/team").ToString();
    }
}

namespace WebHttpAggregator.Services.Teams
{
    public class TeamsApiEndpoints
    {
        private Uri _teamsBaseUri;

        public TeamsApiEndpoints(string teamsBaseUrl)
        {
            _teamsBaseUri = new Uri(teamsBaseUrl);
        }

        public string GetTeams() => new Uri(_teamsBaseUri, $"/api/v1/teams").ToString();
        public string GetTeam(int teamId) => new Uri(_teamsBaseUri, $"/api/v1/teams/{teamId}").ToString();
    }
}

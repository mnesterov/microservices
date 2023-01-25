namespace Players.Application.Commands
{
    public class ChangePlayerTeamCommand : ICommand<bool>
    {
        public ChangePlayerTeamCommand() { }

        public ChangePlayerTeamCommand(int playerId, int? teamId)
        {
            PlayerId = playerId;
            TeamId = teamId;
        }   

        public int PlayerId { get; private set; }
        public int? TeamId { get; private set; }
    }
}

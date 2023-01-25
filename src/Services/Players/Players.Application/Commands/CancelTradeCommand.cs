namespace Players.Application.Commands
{
    public class CancelTradeCommand : ICommand<bool>
    {
        public CancelTradeCommand(int playerId, int? oldTeamId)
        {
            PlayerId = playerId;
            OldTeamId = oldTeamId;
        }

        public int PlayerId { get; private set; }
        public int? OldTeamId { get; private set; }
    }
}

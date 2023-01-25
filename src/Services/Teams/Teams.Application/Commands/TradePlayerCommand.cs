namespace Teams.Application.Commands
{
    public class TradePlayerCommand : ICommand<bool>
    {
        protected TradePlayerCommand() { }

        public TradePlayerCommand(int? oldTeamId, int? newTeamId, int playerId, double playerSalary)
        {
            OldTeamId = oldTeamId;
            NewTeamId = newTeamId;
            PlayerId = playerId;
            PlayerSalary = playerSalary;
        }

        public int? OldTeamId { get; private set; }
        public int? NewTeamId { get; private set; }
        public int PlayerId { get; private set; }
        public double PlayerSalary { get; private set; }
    }
}

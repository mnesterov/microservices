using Players.Domain.Models.PlayerAggregate;
using Xunit;

namespace Players.UnitTests.Domain
{
    public class PlayerAggregateTest
    {
        public PlayerAggregateTest()
        {
        }

        [Fact]
        public void AssignToNewTeam_NewTeamId_Success()
        {
            var player = CreateFakePlayer();
            const int newTeamId = 5;

            player.AssignToNewTeam(newTeamId);

            Assert.Equal(newTeamId, player.TeamId);
        }

        [Fact]
        public void AssignToNewTeam_DomainEvent_Single()
        {
            var player = CreateFakePlayer();
            const int newTeamId = 5;
            const int expectedEvents = 1;

            player.AssignToNewTeam(newTeamId);

            Assert.Equal(expectedEvents, player.DomainEvents.Count);
        }

        #region Private Methods

        private Player CreateFakePlayer()
        {
            var id = 1000;
            var birthday = DateTime.Now;
            var firstName = "FirstName";
            var lastName = "LastName";
            var salary = 30000000;

            var player = new Player(id, birthday, firstName, lastName, salary);

            return player;
        }

        #endregion
    }
}

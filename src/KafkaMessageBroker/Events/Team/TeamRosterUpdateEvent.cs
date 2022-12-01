using Dtos;

namespace KafkaMessageBroker.Events;

public class TeamRosterUpdateEvent : ITeamEvent
{
    public TeamRosterUpdateEvent(TeamRosterDto.UpdateData data)
    {
        Data = data;
    }

    public TeamRosterDto.UpdateData Data { get; set; }
}

using Teams.Domain.Common;

namespace Teams.Domain.Models.CityAggregate;

public class City : EntityWithKey<int>, IAggregateRoot
{
    protected City()
    {
    }

    public City(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; private set; }
}


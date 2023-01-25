using Teams.Domain.Common;
using Teams.Domain.Exceptions;
using Teams.Domain.Models.CityAggregate;

namespace Teams.Domain.Models.TeamAggregate;

public class Team : EntityWithKey<int>, IAggregateRoot
{
    private const double SALARY_CAP = 50000000;

    protected Team()
    {
    }

    public Team(int id, string name, City city)
    {
        Id = id;
        Name = name;

        ChangeCity(city);
    }

    public Team(int id, string name, int cityId)
    {
        Id = id;
        Name = name;
        CityId = cityId;
    }

    public void ChangeCity(City city)
    {
        if (city == null)
            throw new CityArgumentNullDomainException("City cannot be null for a team.");

        CityId = city.Id;
        City = city;
    }

    public string Name { get; private set; }
    public int CityId { get; private set; }
    public City City { get; private set; }
    public string FullName => $"{City.Name} {Name}";
    public double SalaryBasket { get; private set; }
    public double SpaceUnderSalaryCap => SALARY_CAP - SalaryBasket;

    public void UpdateSalaryBasket(double diff)
    {
        if (diff > 0 && SpaceUnderSalaryCap < diff)
            throw new TeamSalaryBasketOverflowDomainException($"Team {Id} salary basket cannot maintain {diff} with space under the cap {SpaceUnderSalaryCap}.");

        SalaryBasket += diff;
    }
}


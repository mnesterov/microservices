using Teams.Domain.Common;
using Teams.Domain.Models.CityAggregate;

namespace Teams.Domain.Repositories;

public interface ICitiesRepository : IRepository<City>
{
    Task<ICollection<City>> GetCitiesAsync();
    Task<City> GetCityAsync(int id);
}

using Microsoft.EntityFrameworkCore;
using Teams.Domain.Common;
using Teams.Domain.Models.CityAggregate;
using Teams.Domain.Repositories;

namespace Teams.Infrastructure.Repositories;

internal class CitiesRepository : ICitiesRepository
{
    private readonly DbSet<City> _cities;
    private readonly AppDbContext _dbContext;

    public IUnitOfWork UnitOfWork => _dbContext;

    public CitiesRepository(AppDbContext dbContext)
    {
        _cities = dbContext.Set<City>();
        _dbContext = dbContext;
    }

    public async Task<ICollection<City>> GetCitiesAsync()
    {
        return await _cities.ToListAsync();
    }

    public async Task<City> GetCityAsync(int id)
    {
        var city = await _cities.FindAsync(id);
        return city;
    }
}

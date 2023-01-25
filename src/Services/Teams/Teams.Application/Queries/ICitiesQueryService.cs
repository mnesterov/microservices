using Teams.Dtos;

namespace Teams.Application.Queries
{
    public interface ICitiesQueryService
    {
        Task<ICollection<CityDto>> GetCitiesAsync();
        Task<CityDto> GetCityAsync(int id);
    }
}

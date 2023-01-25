using Teams.Application.Mappers;
using Teams.Domain.Repositories;
using Teams.Dtos;

namespace Teams.Application.Queries
{
    public class CitiesQueryService : ICitiesQueryService
    {
        private readonly ICitiesRepository _citiesRepository;
        private readonly ITeamsMapper _mapper;

        public CitiesQueryService(ICitiesRepository citiesRepository, ITeamsMapper mapper)
        {
            _citiesRepository = citiesRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CityDto>> GetCitiesAsync()
        {
            var cities = await _citiesRepository.GetCitiesAsync();
            var list = cities.Select(c => _mapper.Map<CityDto>(c)).ToList();
            return list;
        }

        public async Task<CityDto> GetCityAsync(int id)
        {
            var city = await _citiesRepository.GetCityAsync(id);
            var dto = _mapper.Map<CityDto>(city);
            return dto;
        }
    }
}

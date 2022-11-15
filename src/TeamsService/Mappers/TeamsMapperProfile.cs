using AutoMapper;
using Domain.Models;
using Dtos;

namespace TeamsService.Mappers;

public class TeamsMapperProfile : Profile
{
    public TeamsMapperProfile()
    {
        CreateMap<Team, TeamDto>();
        CreateMap<City, CityDto>();
    }
}

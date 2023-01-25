using AutoMapper;
using Teams.Application.Commands;
using Teams.Domain.Models.CityAggregate;
using Teams.Domain.Models.TeamAggregate;
using Teams.Domain.Services;
using Teams.Dtos;

namespace Teams.Application.Mappers
{
    public class TeamsMapperProfile : Profile
    {
        public TeamsMapperProfile()
        {
            CreateMap<Team, TeamDto>();
            CreateMap<City, CityDto>();

            CreateMap<TradePlayerCommand, TradeData>();
        }
    }
}

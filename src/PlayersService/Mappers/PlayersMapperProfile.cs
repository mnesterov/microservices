using AutoMapper;
using Domain.Models;
using Dtos;

namespace PlayersService.Mappers;

public class PlayersMapperProfile : Profile
{
    public PlayersMapperProfile()
    {
        CreateMap<Player, PlayerDto>();
    }
}

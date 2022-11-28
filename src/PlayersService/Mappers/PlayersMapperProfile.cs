using AutoMapper;
using Domain.Models;
using Dtos;

namespace PlayersService.Mappers;

public class PlayersMapperProfile : Profile
{
    public PlayersMapperProfile()
    {
        CreateMap<Player, PlayerDto>();
        CreateMap<PlayerDto.CreateData, Player.CreateData>();
    }
}

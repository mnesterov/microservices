using AutoMapper;
using Players.Application.Commands;
using Players.Application.Dtos;
using Players.Domain.Models.PlayerAggregate;

namespace Players.Application.Mappers
{
    public class PlayersMapperProfile : Profile
    {
        public PlayersMapperProfile()
        {
            CreateMap<Player, PlayerDto>()
                .ForMember(x => x.ContractLength, opt => opt.MapFrom(src => src.SalaryInfo.ContractLength))
                .ForMember(x => x.Salary, opt => opt.MapFrom(src => src.SalaryInfo.ContractAnnualSalary));

            CreateMap<CreatePlayerCommand, Player.CreateData>();
        }
    }
}

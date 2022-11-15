using AutoMapper;

namespace PlayersService.Mappers;

public class PlayersMapper : IPlayersMapper
{
    private readonly IMapper _mapper;

    public PlayersMapper()
    {
        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile<PlayersMapperProfile>();
        });

        mapperConfiguration.AssertConfigurationIsValid();

        _mapper = mapperConfiguration.CreateMapper();
    }

    public T Map<T>(object source) 
    {
        return _mapper.Map<T>(source);
    }
}

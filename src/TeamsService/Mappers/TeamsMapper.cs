using AutoMapper;

namespace TeamsService.Mappers;

public class TeamsMapper : ITeamsMapper
{
    private readonly IMapper _mapper;

    public TeamsMapper()
    {
        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile<TeamsMapperProfile>();
        });

        mapperConfiguration.AssertConfigurationIsValid();

        _mapper = mapperConfiguration.CreateMapper();
    }

    public T Map<T>(object source) 
    {
        return _mapper.Map<T>(source);
    }
}

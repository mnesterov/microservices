namespace TeamsService.Mappers;

public interface ITeamsMapper
{
    T Map<T>(object src);
}

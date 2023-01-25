namespace Teams.Application.Mappers
{
    public interface ITeamsMapper
    {
        T Map<T>(object src);
    }
}
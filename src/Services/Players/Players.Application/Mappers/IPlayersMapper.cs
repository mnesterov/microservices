namespace Players.Application.Mappers
{
    public interface IPlayersMapper
    {
        T Map<T>(object src);
    }
}
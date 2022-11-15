namespace PlayersService.Mappers;

public interface IPlayersMapper
{
    T Map<T>(object src);
}

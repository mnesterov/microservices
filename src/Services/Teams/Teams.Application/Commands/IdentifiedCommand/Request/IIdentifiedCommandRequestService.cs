namespace Teams.Application.Commands.IdentifiedCommand.Request
{
    public interface IIdentifiedCommandRequestService
    {
        Task CreateRequestForCommandAsync<T>(Guid commandId);
        Task<bool> ExistAsync(Guid commandId);
    }
}

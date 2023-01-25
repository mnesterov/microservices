using Microsoft.EntityFrameworkCore;
using Players.Application.Commands.IdentifiedCommand.Request;

namespace Players.Infrastructure.Commands
{
    public class IdentifiedCommandRequestService : IIdentifiedCommandRequestService
    {
        private readonly DbSet<IdentifiedCommandRequest> _requests;
        private readonly AppDbContext _dbContext;

        public IdentifiedCommandRequestService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _requests = dbContext.Set<IdentifiedCommandRequest>();
        }

        public async Task CreateRequestForCommandAsync<T>(Guid commandId)
        {
            var request = new IdentifiedCommandRequest(commandId, typeof(T).Name);
            _requests.Add(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Guid commandId)
        {
            var request = await _requests.FindAsync(commandId);
            return request != null;
        }
    }
}

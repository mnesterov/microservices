using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Players.Application.IntegrationEvents;

namespace Players.Infrastructure.PipelineBehaviors
{
    public class CommandPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<CommandPipelineBehavior<TRequest, TResponse>> _logger;

        public CommandPipelineBehavior(
            AppDbContext dbContext,
            IIntegrationEventService integrationEventService,
            ILogger<CommandPipelineBehavior<TRequest, TResponse>> logger
            )
        {
            _dbContext = dbContext;
            _integrationEventService = integrationEventService;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_dbContext.TransactionInProgress)
            {
                return await next();
            }

            var response = default(TResponse);
            var commandType = typeof(TResponse).Name;

            try
            {
                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    {
                        transactionId = transaction.TransactionId;

                        _logger.LogInformation("--> Begin transaction {TransactionId} for {CommandType} ({@Command})", transaction.TransactionId, commandType, request);

                        response = await next();

                        _logger.LogInformation("--> Commit transaction {TransactionId} for {CommandType})", transaction.TransactionId, commandType);

                        await _dbContext.CommitTransactionAsync();
                    }

                    await _integrationEventService.PublishEventsAsync(transactionId);
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ERROR for {CommandType} ({@Command})", commandType, request);
            }

            return response;
        }
    }
}

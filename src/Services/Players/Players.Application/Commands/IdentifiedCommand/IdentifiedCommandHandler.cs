using MediatR;
using Microsoft.Extensions.Logging;
using Players.Application.Commands.IdentifiedCommand.Request;

namespace Players.Application.Commands.IdentifiedCommand
{
    public class IdentifiedCommandHandler<TCommand, TResponse> : ICommandHandler<IdentifiedCommand<TCommand, TResponse>, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private readonly IMediator _mediator;
        private readonly ILogger<IdentifiedCommandHandler<TCommand, TResponse>> _logger;
        private readonly IIdentifiedCommandRequestService _identifiedCommandRequestService;

        public IdentifiedCommandHandler(
            IIdentifiedCommandRequestService identifiedCommandRequestService,
            ILogger<IdentifiedCommandHandler<TCommand, TResponse>> logger,
            IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
            _identifiedCommandRequestService = identifiedCommandRequestService;
        }

        public async Task<TResponse> Handle(IdentifiedCommand<TCommand, TResponse> identifiedCommand, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync();
            try
            {
                var exist = await _identifiedCommandRequestService.ExistAsync(identifiedCommand.Id);
                if (exist)
                {
                    return default(TResponse);
                }
                await _identifiedCommandRequestService.CreateRequestForCommandAsync<TCommand>(identifiedCommand.Id);
            }
            finally
            {
                _semaphore.Release();
            }

            _logger.LogInformation($"--> Executing Command {identifiedCommand.Command.GetType()} with ID {identifiedCommand.Id}");

            //dispatch the inner command to execute
            var result = await _mediator.Send(identifiedCommand.Command);

            _logger.LogInformation("--> Command {CommandType} with ID {CommandId} result: {@Result}",
                identifiedCommand.Command.GetType(),
                identifiedCommand.Id,
                result
                );

            return result;
        }
    }
}

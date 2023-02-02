using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Players.Application.Commands;
using Players.Application.Commands.IdentifiedCommand;
using Players.Application.Commands.IdentifiedCommand.Request;
using Xunit;

namespace Players.UnitTests.Application
{
    public class IdentifiedCommandHandlerTest
    {
        private readonly Mock<IIdentifiedCommandRequestService> _requestService;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<IdentifiedCommandHandler<ChangePlayerTeamCommand, bool>>> _logger;

        public IdentifiedCommandHandlerTest()
        {
            _requestService = new Mock<IIdentifiedCommandRequestService>();
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<IdentifiedCommandHandler<ChangePlayerTeamCommand, bool>>>();
        }

        [Fact]
        public async void Send_DuplicateHandle_AlreadyExists()
        {
            var commandId = Guid.NewGuid();
            var command = new ChangePlayerTeamCommand(1, 1);
            var identifiedCommand = new IdentifiedCommand<ChangePlayerTeamCommand, bool>(command, commandId);
            var cancellationToken = default(CancellationToken);

            _requestService.Setup(s => s.ExistAsync(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            _mediator.Setup(s => s.Send(command, cancellationToken)).Returns(Task.FromResult(true));

            var handler = new IdentifiedCommandHandler<ChangePlayerTeamCommand, bool>(_requestService.Object, _logger.Object, _mediator.Object);
            var result = await handler.Handle(identifiedCommand, cancellationToken);

            Assert.False(result);
            _mediator.Verify(x => x.Send(It.IsAny<IRequest<bool>>(), cancellationToken), Times.Never());
        }
    }
}

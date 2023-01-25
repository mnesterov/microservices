using MediatR;

namespace Players.Application.Commands.IdentifiedCommand
{
    public class IdentifiedCommand<TCommand, TResponse> : ICommand<TResponse> where TCommand : ICommand<TResponse>
    {
        public IdentifiedCommand(TCommand command, Guid id)
        {
            Command = command;
            Id = id;
        }

        public Guid Id { get; private set; }
        public TCommand Command { get; private set; }
    }
}

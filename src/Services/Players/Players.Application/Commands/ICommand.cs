using MediatR;

namespace Players.Application.Commands
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}

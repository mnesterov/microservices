using MediatR;

namespace Teams.Application.Commands
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}

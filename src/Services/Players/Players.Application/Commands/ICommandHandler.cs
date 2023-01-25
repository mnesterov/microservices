﻿using MediatR;

namespace Players.Application.Commands
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
        where TCommand : ICommand<TResponse>
    {
    }
}

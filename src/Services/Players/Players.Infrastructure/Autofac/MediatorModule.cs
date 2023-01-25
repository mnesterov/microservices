using Autofac;
using MediatR;
using Players.Application.Commands;
using Players.Application.DomainEventHandlers;
using Players.Infrastructure.PipelineBehaviors;

namespace Players.Infrastructure.Autofac
{
    public class MediatorModule : Module
    {
        public MediatorModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            //register domain event handlers
            builder.RegisterAssemblyTypes(typeof(IDomainEventHandler<>).Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            //register command handlers
            builder.RegisterAssemblyTypes(typeof(ICommandHandler<,>).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => 
                { 
                    object o; 
                    return componentContext.TryResolve(t, out o) ? o : null; 
                };
            });

            builder.RegisterGeneric(typeof(CommandPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}

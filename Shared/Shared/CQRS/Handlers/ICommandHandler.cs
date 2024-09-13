namespace Shared.CQRS.Handlers;

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
	where TCommand : ICommand<Unit>, new()
{

}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
	where TCommand : ICommand<TResponse>
	where TResponse : notnull
{

}

namespace Shared.CQRS.Models;

public interface ICommand : IRequest<Unit>
{

}

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}

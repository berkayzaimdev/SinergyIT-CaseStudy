namespace Shared.CQRS.Models;

public interface IQuery<out TResult> : IRequest<TResult>
	where TResult : notnull
{

}

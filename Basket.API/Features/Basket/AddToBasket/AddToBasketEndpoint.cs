
namespace Basket.API.Features.Basket.AddToBasket;

public record AddToBasketRequest(string ProductId) : ICommand<AddToBasketResult>;
public record AddToBasketResponse(bool IsSuccess);
public class AddToBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		throw new NotImplementedException();
	}
}

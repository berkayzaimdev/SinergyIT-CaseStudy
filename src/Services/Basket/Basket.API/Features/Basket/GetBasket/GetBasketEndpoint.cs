using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json;

namespace Basket.API.Features.Basket.GetBasket;

public record GetBasketResponse(ShoppingCartDto ShoppingCart);
public class GetBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/get-basket", [Authorize] async (HttpContext context, ISender sender) =>
		{
			var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userId))
			{
				return Results.BadRequest("User ID not found!");
			}

			var result = await sender.Send(new GetBasketQuery(userId));

			var response = result.Adapt<ShoppingCartDto>();

			return Results.Ok(response);
		});
	}
}

public record GetBasketQuery(string UserId) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCartDto ShoppingCart);
public class GetBasketHandler 
	(IDistributedCache distributedCache)
	: IQueryHandler<GetBasketQuery, GetBasketResult>
{
	public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
	{
		var cachedValue = await distributedCache.GetStringAsync(query.UserId);

        if (string.IsNullOrEmpty(cachedValue))
        {
			return new(new ShoppingCartDto());
        }

		var values = JsonSerializer.Deserialize<ShoppingCartDto>(cachedValue);

		if (values is null)
		{
			return new(new ShoppingCartDto()); // TODO: error message
		}

		return new(values);
	}
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Features.Basket.AddToBasket;

//public record AddToBasketRequest(string ProductId);
public record AddToBasketResponse(bool IsSuccess);
public class AddToBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/add-to-basket", [Authorize] (HttpContext context, string productId) =>
		{
			var userId = context.User.FindFirst("userId")?.Value;

			if (userId == null)
			{
				return Results.BadRequest("User ID not found.");
			}

			return Results.Ok(new { UserId = userId, ProductId = productId });
		});
	}
}
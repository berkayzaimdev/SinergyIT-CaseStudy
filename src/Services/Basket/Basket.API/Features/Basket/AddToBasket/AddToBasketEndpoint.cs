using System.Security.Claims;

namespace Basket.API.Features.Basket.AddToBasket;

//public record AddToBasketRequest(string ProductId);
public record AddToBasketResponse(bool IsSuccess);
[ApiController]
public class AddToBasketEndpoint : ControllerBase
{
	private readonly IHttpContextAccessor _accessor;
	private readonly IPublishEndpoint _publishEndpoint;

	public AddToBasketEndpoint(IHttpContextAccessor accessor, IPublishEndpoint publishEndpoint)
	{
		_accessor = accessor;
		_publishEndpoint = publishEndpoint;
	}

	[HttpPost]
	[Authorize]
	[Route("/add-to-basket/{productId}")]
	public async Task<IActionResult> AddToBasketAsync([FromRoute] string productId)
	{
		var context = _accessor.HttpContext;

		if (context is null)
		{
			return BadRequest("HttpContext is unaccessable");
		}

		var userId = _accessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

		if (string.IsNullOrEmpty(userId))
		{
			return BadRequest("User ID not found!");
		}

		var message = new AddToBasketEvent(userId, productId);

		await _publishEndpoint.Publish(message);

		return Ok(new { UserId = userId, ProductId = productId });
	}
}


//public void AddRoutes(IEndpointRouteBuilder app)
//{
//	app.MapPost("/add-to-basket/{productId}", [Authorize] async (HttpContext context, IPublishEndpoint _publishEndpoint, [FromRoute] string productId) =>
//	{
//		var userId = context.User.FindFirst("userId")?.Value;

//		if (string.IsNullOrEmpty(userId))
//		{
//			return Results.BadRequest("User ID not found!");
//		}

//		var message = new AddToBasketEvent(userId, productId);

//		await _publishEndpoint.Publish(message);

//		return Results.Ok(new { UserId = userId, ProductId = productId });
//	});
//}
namespace Catalog.API.Features.Products.GetAll;

// public record GetAllProductsRequest();
public record GetAllProductsResponse(IEnumerable<GetAllProductsDto> Products);
public class GetAllProductsEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/", async (ISender _sender) =>
		{
			var result = await _sender.Send(new GetAllProductsQuery());

			var response = result.Adapt<GetAllProductsResponse>();

			return Results.Ok(response);
		});
	}
}

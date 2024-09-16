namespace Catalog.API.Features.Products.GetAllByBrandId;

// public record GetAllProductsByBrandIdRequest();
public record GetAllProductsByBrandIdResponse(IEnumerable<GetAllProductsByBrandIdDto> Products);
public class GetAllProductsByBrandIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("brands/{brandId}/products", async (string brandId, ISender _sender) =>
		{
			var result = await _sender.Send(new GetAllProductsByBrandIdQuery(brandId));

			var response = result.Adapt<GetAllProductsByBrandIdResponse>();

			return Results.Ok(response);
		});
	}
}
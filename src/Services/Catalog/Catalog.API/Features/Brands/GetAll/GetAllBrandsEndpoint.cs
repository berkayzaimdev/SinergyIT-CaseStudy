namespace Catalog.API.Features.Brands.GetAll;

public record GetAllBrandsResponse(IEnumerable<GetAllBrandsDto> Brands);
public class GetAllBrandsEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/brands/", async (ISender _sender) =>
		{
			var result = await _sender.Send(new GetAllBrandsQuery());

			var response = result.Adapt<GetAllBrandsResponse>();

			return Results.Ok(response);
		});
	}
}

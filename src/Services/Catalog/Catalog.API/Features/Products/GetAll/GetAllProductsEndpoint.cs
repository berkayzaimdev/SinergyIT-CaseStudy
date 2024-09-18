using Catalog.API.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Products.GetAll;

// public record GetAllProductsRequest();
public record GetAllProductsResponse(IEnumerable<GetAllProductsDto> Products,
	PaginationResponse PaginationResponse);
public class GetAllProductsEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/", async (
			[FromQuery] int? pageNumber,
			[FromQuery] int? pageSize,
			ISender _sender) =>
		{
			PaginationRequest paginationRequest = new()
			{
				PageNumber = pageNumber ?? 1,
				PageSize = pageSize ?? 6
			};

			var result = await _sender.Send(new GetAllProductsQuery(paginationRequest));

			var response = result.Adapt<GetAllProductsResponse>();

			return Results.Ok(response);
		});
	}
}

using Catalog.API.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Features.Products.GetAllByBrandId;

// public record GetAllProductsByBrandIdRequest();
public record GetAllProductsByBrandIdResponse(
	IEnumerable<GetAllProductsByBrandIdDto> Products,
	PaginationResponse PaginationResponse);
public class GetAllProductsByBrandIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("brands/{brandId}/products", async (
			[FromRoute] string brandId,
			[FromQuery] int? pageNumber,
			[FromQuery] int? pageSize,
			ISender _sender) =>
		{
			PaginationRequest paginationRequest = new()
			{
				PageNumber = pageNumber ?? 1,
				PageSize = pageSize ?? 6  
			};

			var result = await _sender.Send(new GetAllProductsByBrandIdQuery(brandId, paginationRequest));

			var response = result.Adapt<GetAllProductsByBrandIdResponse>();

			return Results.Ok(response);
		});
	}
}
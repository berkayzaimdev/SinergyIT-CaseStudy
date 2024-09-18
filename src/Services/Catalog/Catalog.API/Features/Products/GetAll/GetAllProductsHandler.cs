using Catalog.API.Models.Pagination;

namespace Catalog.API.Features.Products.GetAll;

public record GetAllProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetAllProductsResult>;
public record GetAllProductsResult(
	IEnumerable<GetAllProductsDto> Products,
	PaginationResponse PaginationResponse);
public record GetAllProductsDto(
	string Id,
	string Name,
	decimal Price,
	string BrandId,
	string BrandName
);

public class GetAllProductsHandler
	(IMongoService mongoService)
	: IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
{
	public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
	{
		var brandCollection = mongoService.GetCollection<Brand>();
		var brands = await brandCollection.Find(_ => true).ToListAsync();
		var brandMap = brands.ToDictionary(b => b.Id, b => b.Name);

		var products = mongoService.GetCollection<Product>()
			.Find(_ => true);

		var productsCount = await products.CountDocumentsAsync(cancellationToken);

		if (products is null || productsCount == 0)
        {
			return new GetAllProductsResult(Enumerable.Empty<GetAllProductsDto>(), new());
        }

        var result = products
			.Skip((request.PaginationRequest.PageNumber - 1) * request.PaginationRequest.PageSize)
			.Limit(request.PaginationRequest.PageSize)
			.ToEnumerable(cancellationToken)
			.Select(product =>
			{
				var brandname = brandMap.TryGetValue(product.BrandId, out var name) ? name : "Unknown";

				return new GetAllProductsDto
				(
					product.Id.ToString(),
					product.Name,
					product.Price,
					product.BrandId.ToString(),
					brandname
				);
			});

		return new GetAllProductsResult(result, new(
			request.PaginationRequest.PageNumber,
			request.PaginationRequest.PageSize,
			(int)Math.Round(productsCount / (double)request.PaginationRequest.PageSize)));
	}
}

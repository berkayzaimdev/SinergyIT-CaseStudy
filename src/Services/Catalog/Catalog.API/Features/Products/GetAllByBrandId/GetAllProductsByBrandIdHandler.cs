using Catalog.API.Models.Pagination;

namespace Catalog.API.Features.Products.GetAllByBrandId;

public record GetAllProductsByBrandIdQuery(
	string BrandId,
	PaginationRequest PaginationRequest) : IQuery<GetAllProductsByBrandIdResult>;
public record GetAllProductsByBrandIdResult(
	IEnumerable<GetAllProductsByBrandIdDto> Products,
	PaginationResponse PaginationResponse);
public record GetAllProductsByBrandIdDto(
	string Id,
	string Name,
	decimal Price,
	string BrandId,
	string BrandName
);

public class GetAllProductsByBrandIdHandler
	(IMongoService mongoService)
	: IQueryHandler<GetAllProductsByBrandIdQuery, GetAllProductsByBrandIdResult>
{
	public async Task<GetAllProductsByBrandIdResult> Handle(GetAllProductsByBrandIdQuery request, CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.BrandId, out var brandId))
		{
			throw new ArgumentException("Geçersiz GUID formatı", nameof(request.BrandId));
		}

		var products = mongoService.GetCollection<Product>()
			.Find(product => product.BrandId.Equals(brandId));

		var brandCollection = mongoService.GetCollection<Brand>();
		var brands = await brandCollection.Find(_ => true).ToListAsync();
		var brandMap = brands.ToDictionary(b => b.Id, b => b.Name);

		var productsCount = await products.CountDocumentsAsync(cancellationToken);

		if (products is null || productsCount == 0)
		{
			return new GetAllProductsByBrandIdResult(Enumerable.Empty<GetAllProductsByBrandIdDto>(), new());
		}

		var result = products
			.Skip((request.PaginationRequest.PageNumber - 1) * request.PaginationRequest.PageSize)
			.Limit(request.PaginationRequest.PageSize)
			.ToEnumerable(cancellationToken)
			.Select(product =>
			{
				var brandname = brandMap.TryGetValue(product.BrandId, out var name) ? name : "Unknown";

				return new GetAllProductsByBrandIdDto
				(
					product.Id.ToString(),
					product.Name,
					product.Price,
					product.BrandId.ToString(),
					brandname
				);
			});

		return new GetAllProductsByBrandIdResult(result, new(
			request.PaginationRequest.PageNumber,
			request.PaginationRequest.PageSize,
			(int)Math.Round(productsCount / (double)request.PaginationRequest.PageSize)));
	}
}

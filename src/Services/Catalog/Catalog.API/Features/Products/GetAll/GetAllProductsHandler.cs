namespace Catalog.API.Features.Products.GetAll;

public record GetAllProductsQuery() : IQuery<GetAllProductsResult>;
public record GetAllProductsResult(IEnumerable<GetAllProductsDto> Products);
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
		var productCollection = mongoService.GetCollection<Product>();
		var brandCollection = mongoService.GetCollection<Brand>();


		var brands = await brandCollection.Find(_ => true).ToListAsync();
		var brandMap = brands.ToDictionary(b => b.Id, b => b.Name);

		var products = await productCollection.Find(_ => true).ToListAsync();

        if (products is null || products.Count == 0)
        {
			return new GetAllProductsResult(Enumerable.Empty<GetAllProductsDto>());
        }

        var result = products.Select(product =>
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

		return new GetAllProductsResult(result);
	}
}

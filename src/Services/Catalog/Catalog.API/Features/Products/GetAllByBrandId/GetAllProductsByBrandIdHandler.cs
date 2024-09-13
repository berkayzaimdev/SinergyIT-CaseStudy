namespace Catalog.API.Features.Products.GetAllByBrandId;

public record GetAllProductsByBrandIdQuery(string BrandId) : IQuery<GetAllProductsByBrandIdResult>;
public record GetAllProductsByBrandIdResult(IEnumerable<GetAllProductsByBrandIdDto> Products);
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
		var products = mongoService.GetCollection<Product>().Find(product => product.BrandId.Equals(Guid.Parse(request.BrandId)));

		var brandCollection = mongoService.GetCollection<Brand>();
		var brands = await brandCollection.Find(_ => true).ToListAsync();
		var brandMap = brands.ToDictionary(b => b.Id, b => b.Name);

		if (products is null || await products.CountDocumentsAsync(cancellationToken) == 0)
		{
			return new GetAllProductsByBrandIdResult(Enumerable.Empty<GetAllProductsByBrandIdDto>());
		}

		var result = products.ToEnumerable(cancellationToken).Select(product =>
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

		return new GetAllProductsByBrandIdResult(result);
	}
}

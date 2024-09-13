namespace Catalog.API.Features.Brands.GetAll;

public record GetAllBrandsQuery() : IQuery<GetAllBrandsResult>;
public record GetAllBrandsResult(IEnumerable<GetAllBrandsDto> Brands);
public record GetAllBrandsDto(
	string Id,
	string Name
);

public class GetAllBrandsHandler
	(IMongoService mongoService)
	: IQueryHandler<GetAllBrandsQuery, GetAllBrandsResult>
{
	public async Task<GetAllBrandsResult> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
	{
		var brandCollection = mongoService.GetCollection<Brand>();

		var brands = brandCollection.Find(_ => true);

		if (brands is null || await brands.CountDocumentsAsync(cancellationToken) == 0)
		{
			return new GetAllBrandsResult([]);
		}

		var result = brands.ToEnumerable(cancellationToken).Select(brand =>
		{
			return new GetAllBrandsDto
			(
				brand.Id.ToString(),
				brand.Name
			);
		});

		return new GetAllBrandsResult(result);
	}
}

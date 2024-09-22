using Catalog.API.Models.Pagination;

namespace Catalog.API.Features.Products.GetAllByBrandId;

public class GetAllProductsByBrandIdHandlerTests
{
    private readonly Mock<IMongoService> _mongoServiceMock;

    public GetAllProductsByBrandIdHandlerTests()
    {
        _mongoServiceMock = new();
    }

    [Fact]
    public async Task Handle_Should_ThrowFormatException_WhenBrandIdInvalid()
    {
        var query = new GetAllProductsByBrandIdQuery("notguid", new());

        var handler = new GetAllProductsByBrandIdHandler(_mongoServiceMock.Object);

        await FluentActions
            .Invoking(() => handler.Handle(query, default))
            .Should()
            .ThrowAsync<FormatException>();
    }
}

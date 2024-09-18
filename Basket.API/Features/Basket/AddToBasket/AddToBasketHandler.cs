using Basket.API.Models;
using Catalog.API.Models;
using Catalog.API.Services.Concrete;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using System.Text.Json;

namespace Basket.API.Features.Basket.AddToBasket;

public record AddToBasketCommand(string UserId, AddToBasketDto AddToBasketDto) : ICommand<AddToBasketResult>;
public record AddToBasketResult(bool IsSuccess);
public record AddToBasketDto
(
	string ProductId,
	int Quantity
);

public class AddToBasketHandler 
	(IDistributedCache cache, MongoService mongoService)
	: ICommandHandler<AddToBasketCommand, AddToBasketResult>
{
	public async Task<AddToBasketResult> Handle(AddToBasketCommand command, CancellationToken cancellationToken)
	{
        if (!(await IsProductInStock(Guid.Parse(command.AddToBasketDto.ProductId))))
        {
			return new(false);
        }

        var cachedBasket = await cache.GetStringAsync(command.UserId, cancellationToken);

		ShoppingCart shoppingCart;

		shoppingCart = string.IsNullOrEmpty(cachedBasket) ? new() : JsonSerializer.Deserialize<ShoppingCart>(cachedBasket) ?? throw new Exception("error while trying to fetch shoppingcart from cache");

        shoppingCart.Items.Add(new ShoppingCartItem
		{
			ProductId = Guid.Parse(command.AddToBasketDto.ProductId),
			Quantity = command.AddToBasketDto.Quantity,
		});

		await cache.SetStringAsync(command.UserId, JsonSerializer.Serialize<ShoppingCart>(shoppingCart));

		return new(true);
	}

	private async Task<bool> IsProductInStock(Guid productId) => 
		await mongoService.GetCollection<Stock>()
			.Find(p => p.Id == productId && p.Count > 0)
			.CountDocumentsAsync() > 0;
}

namespace Catalog.API.Consumers;

public class AddToBasketEventConsumer
	(IDistributedCache cache, ISendEndpointProvider sendEndpointProvider, IMongoService mongoService)
	: IConsumer<AddToBasketEvent> 
{
    public async Task Consume(ConsumeContext<AddToBasketEvent> context)
	{
		if (!(Guid.TryParse(context.Message.ProductId, out Guid productId) || !(await IsProductInStockAsync(productId))))
		{
			await SendFailedMessage();
		}

        else
        {
			await IncreaseQuantityAsync(context.Message.UserId, productId);
		} 
	}

	private async Task<bool> IsProductInStockAsync(Guid productId) =>
	await mongoService.GetCollection<Stock>()
		.Find(p => p.Id == productId && p.Count > 0)
		.CountDocumentsAsync() > 0;


	private async Task IncreaseQuantityAsync(string userId, Guid productId)
	{
		var cachedBasket = await cache.GetStringAsync(userId);
		ShoppingCartDto? shoppingCart;
		ShoppingCartItemDto? item;

		if (!string.IsNullOrEmpty(cachedBasket))
		{
			shoppingCart = JsonSerializer.Deserialize<ShoppingCartDto>(cachedBasket);

			if (shoppingCart is null)
			{
				await SendFailedMessage();
			}

			item = shoppingCart!.Items.FirstOrDefault(x => x.ProductId == productId);

			if (item is null)
			{
				await SendFailedMessage();
			}

            var stock = (await (await mongoService.GetCollection<Stock>().FindAsync(p => p.Id == productId)).FirstOrDefaultAsync()).Count;

			if (item!.Quantity >= stock)
			{
				await SendFailedMessage();
			}

			item.Quantity++;
		}

		else 
		{
			shoppingCart = new();

			item = new ShoppingCartItemDto()
			{
				ProductId = productId,
				Quantity = 1
			};

			shoppingCart.Items.Add(item);
		}
		
		await cache.SetStringAsync(userId, JsonSerializer.Serialize<ShoppingCartDto>(shoppingCart));

		var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQSettings.Catalog_AddToBasketSucceededEventQueue}"));
		await sendEndpoint.Send((new AddToBasketSucceededMessage(item.ProductId.ToString(), item.Quantity)));
	}

	private async Task SendFailedMessage()
	{
		var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQSettings.Catalog_AddToBasketFailedEventQueue}"));
		await sendEndpoint.Send((new AddToBasketFailedMessage()));
	}
}

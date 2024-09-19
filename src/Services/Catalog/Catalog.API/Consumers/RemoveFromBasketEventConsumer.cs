namespace Catalog.API.Consumers;

public class RemoveFromBasketEventConsumer
	(IDistributedCache cache, ISendEndpointProvider sendEndpointProvider, MongoService mongoService)
	: IConsumer<RemoveFromBasketEvent>
{
	public async Task Consume(ConsumeContext<RemoveFromBasketEvent> context)
	{
		if (!(Guid.TryParse(context.Message.ProductId, out Guid productId) || !(await IsProductExistAsync(productId))))
		{
			await SendFailedMessage();
		}

		else
		{
			await ReduceQuantityAsync(context.Message.UserId, productId);
		}
	}

	private async Task<bool> IsProductExistAsync(Guid productId) =>
	await mongoService.GetCollection<Stock>()
		.Find(p => p.Id == productId)
		.CountDocumentsAsync() > 0;
	private async Task ReduceQuantityAsync(string userId, Guid productId)
	{
		string? cachedBasket = await cache.GetStringAsync(userId);
		
		if(string.IsNullOrEmpty(cachedBasket))
		{
			await SendFailedMessage();
		}


		ShoppingCartDto? shoppingCart = JsonSerializer.Deserialize<ShoppingCartDto>(cachedBasket!);

        if (shoppingCart is null)
        {
			await SendFailedMessage();
		}

		ShoppingCartItemDto? item = shoppingCart!.Items.FirstOrDefault(x => x.ProductId == productId);

		if (item is null)
		{
			await SendFailedMessage();
		}

		if (item!.Quantity <= 1)
		{
			shoppingCart.Items.Remove(item);
		}

		else
		{
			item.Quantity--;
		}

		await cache.SetStringAsync(userId, JsonSerializer.Serialize<ShoppingCartDto>(shoppingCart));

		var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQSettings.Catalog_RemoveFromBasketSucceededEventQueue}"));
		await sendEndpoint.Send((new RemoveFromBasketSucceededMessage(item.ProductId.ToString(), item.Quantity)));
	}

	private async Task SendFailedMessage()
	{
		var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new($"queue:{RabbitMQSettings.Catalog_RemoveFromBasketFailedEventQueue}"));
		await sendEndpoint.Send((new RemoveFromBasketFailedMessage()));
	}
}
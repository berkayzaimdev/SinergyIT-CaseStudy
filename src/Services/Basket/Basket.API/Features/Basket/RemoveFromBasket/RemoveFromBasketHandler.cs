namespace Basket.API.Features.Basket.RemoveFromBasket;

public record RemoveFromBasketCommand(string UserId, string ProductId) : ICommand<RemoveFromBasketResult>;
public record RemoveFromBasketResult(bool IsSuccess);

public class RemoveFromBasketHandler;
//public class RemoveFromBasketHandler 
//	(IDistributedCache cache, MongoService mongoService)
//	: ICommandHandler<RemoveFromBasketCommand, RemoveFromBasketResult>
//{
//	public async Task<RemoveFromBasketResult> Handle(RemoveFromBasketCommand command, CancellationToken cancellationToken)
//	{
//        if (Guid.TryParse(command.ProductId,out Guid productId))
//        {
//			return new(false); // TODO: error message
//		}

//        if (!(await IsProductExistAsync(productId)))
//        {
//			return new(false);
//        } // TODO: may be removed after implementing the message queue

//		await ReduceQuantityAsync(command.UserId, productId, cancellationToken);

//		return new(true);
//	}

//	private async Task<bool> IsProductExistAsync(Guid productId) =>
//	await mongoService.GetCollection<Stock>()
//		.Find(p => p.Id == productId)
//		.CountDocumentsAsync() > 0;

//	private async Task ReduceQuantityAsync(string userId, Guid productId, CancellationToken cancellationToken)
//	{
//		var cachedBasket = await cache.GetStringAsync(userId, cancellationToken) ?? throw new Exception("error while trying to fetch shoppingcart from cache");

//		ShoppingCart shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket) ?? throw new Exception("error while trying to fetch shoppingcart from cache");

//		ShoppingCartItem item = shoppingCart.Items.FirstOrDefault(x => x.ProductId == productId) ?? throw new Exception("error while trying to fetch shoppingcart from cache");

//		if (item.Quantity <= 1)
//		{
//			shoppingCart.Items.Remove(item);
//		}

//		else
//		{
//			item.Quantity--;
//		}

//		await cache.SetStringAsync(userId, JsonSerializer.Serialize<ShoppingCart>(shoppingCart));
//	}
//}

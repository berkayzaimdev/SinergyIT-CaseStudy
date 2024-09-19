namespace Basket.API.Features.Basket.AddToBasket;

public record AddToBasketCommand(string UserId, string ProductId) : ICommand<AddToBasketResult>;
public record AddToBasketResult(bool IsSuccess);
public class AddToBasketHandler;
//public class AddToBasketHandler 
//	(IDistributedCache cache, MongoService mongoService)
//	: ICommandHandler<AddToBasketCommand, AddToBasketResult>
//{
//	public async Task<AddToBasketResult> Handle(AddToBasketCommand command, CancellationToken cancellationToken)
//	{
//		if (Guid.TryParse(command.ProductId, out Guid productId))
//		{
//			return new(false); // TODO: error message
//		}

//		if (!(await IsProductInStockAsync(productId)))
//		{
//			return new(false);
//		} // TODO: may be removed after implementing the message queue


//		await IncreaseQuantityAsync(command.UserId, productId, cancellationToken);

//		return new(true);
//	}

//	private async Task<bool> IsProductInStockAsync(Guid productId) =>
//	await mongoService.GetCollection<Stock>()
//		.Find(p => p.Id == productId && p.Count > 0)
//		.CountDocumentsAsync() > 0;


//	private async Task IncreaseQuantityAsync(string userId, Guid productId, CancellationToken cancellationToken)
//	{
//		var cachedBasket = await cache.GetStringAsync(userId, cancellationToken);

//		ShoppingCart shoppingCart;

//        if (!string.IsNullOrEmpty(cachedBasket)) // if the user has shoppingcart in the cache
//        {
//			shoppingCart = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket) ?? throw new Exception("error while trying to fetch shoppingcart from cache");

//			ShoppingCartItem item = shoppingCart.Items.FirstOrDefault(x => x.ProductId == productId) ?? throw new Exception("error while trying to fetch shoppingcart from cache");

//			var stock = (await (await mongoService.GetCollection<Stock>().FindAsync(p => p.Id == productId)).FirstOrDefaultAsync()).Count;

//			if (item.Quantity >= stock) 
//			{
//				throw new Exception("stock limit error");
//			}

//			item.Quantity++;	
//		}

//        else // if user has nothing in its shoppingcart
//        {
//			shoppingCart = new();

//			ShoppingCartItem item = new()
//			{
//				ProductId = productId,
//				Quantity = 1
//			};
//		}

//		await cache.SetStringAsync(userId, JsonSerializer.Serialize<ShoppingCart>(shoppingCart));
//	}
//}

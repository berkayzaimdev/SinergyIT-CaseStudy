namespace Basket.API.Models;

public class ShoppingCart
{
	public ICollection<ShoppingCartItem> Items { get; set; } = default!;
}

public class ShoppingCartItem
{
    public Guid ProductId { get; set; } = Guid.Empty;
    public int Quantity { get; set; }
}
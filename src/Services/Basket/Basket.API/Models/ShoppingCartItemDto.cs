namespace Basket.API.Models;

public class ShoppingCartItemDto
{
    public Guid ProductId { get; set; } = Guid.Empty;
    public int Quantity { get; set; }
}
namespace Basket.API.Models;

public class ShoppingCartDto
{
    public ICollection<ShoppingCartItemDto> Items { get; set; } = [];
}

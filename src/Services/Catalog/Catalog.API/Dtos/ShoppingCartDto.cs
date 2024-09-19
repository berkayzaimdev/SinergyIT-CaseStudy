namespace Catalog.API.Dtos;

public class ShoppingCartDto
{
	public ICollection<ShoppingCartItemDto> Items { get; set; } = default!;
}

public class ShoppingCartItemDto
{
	public Guid ProductId { get; set; } = Guid.Empty;
	public int Quantity { get; set; }
}
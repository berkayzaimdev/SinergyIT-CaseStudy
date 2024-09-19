namespace Shared.Messages;

public record RemoveFromBasketSucceededMessage
(
	string ProductId,
	int Quantity
)
{
	const string Message = "Stoktan ürün azaltma işlemi başarılı";
}

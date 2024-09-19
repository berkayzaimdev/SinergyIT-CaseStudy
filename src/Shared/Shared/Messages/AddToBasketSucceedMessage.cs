namespace Shared.Messages;

public record AddToBasketSucceededMessage
(
    string ProductId,
	int Quantity
)
{
	const string Message = "Stoğa ekleme işlemi başarılı";
}

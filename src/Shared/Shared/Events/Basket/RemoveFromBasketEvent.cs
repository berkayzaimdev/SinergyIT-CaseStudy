namespace Shared.Events.Basket;

public record RemoveFromBasketEvent(
	string UserId,
	string ProductId);
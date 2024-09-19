namespace Shared.Events.Basket;

public record AddToBasketEvent(
	string UserId,
	string ProductId);
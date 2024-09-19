namespace Shared;

public static class RabbitMQSettings
{
    public const string Catalog_AddToBasketSucceededEventQueue = "catalog-add-to-basket-succeeded-event-queue";
    public const string Catalog_AddToBasketFailedEventQueue = "catalog-add-to-basket-failed-event-queue";

	public const string Catalog_RemoveFromBasketSucceededEventQueue = "catalog-remove-from-basket-succeeded-event-queue";
	public const string Catalog_RemoveFromBasketFailedEventQueue = "catalog-remove-from-basket-failed-event-queue";
}

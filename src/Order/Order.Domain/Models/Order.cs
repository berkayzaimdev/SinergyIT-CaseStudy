namespace Order.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<OrderItem> Items { get; set; } = [];
}

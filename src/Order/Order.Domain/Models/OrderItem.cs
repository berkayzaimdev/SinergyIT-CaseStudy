﻿namespace Order.Domain.Models;

public class OrderItem
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

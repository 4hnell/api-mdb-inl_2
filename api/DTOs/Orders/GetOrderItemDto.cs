namespace api.DTOs.Orders;

public class GetOrderItemDto
{
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal LineSum { get; set; }
}
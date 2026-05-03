namespace api.DTOs.Orders;

public abstract class BaseOrderDto
{
    public required string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public required string StoreName { get; set; }
}
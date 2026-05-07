namespace api.DTOs.Orders;

public abstract class BaseOrderDto
{
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
}

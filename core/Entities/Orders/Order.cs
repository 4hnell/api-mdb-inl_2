namespace core.Entities.Orders;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public required string OrderNumber { get; set; }
    public Customer Customer { get; set; } = default!;
    public List<OrderItem> OrderItems { get; set; } = [];
}

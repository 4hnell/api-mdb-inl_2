namespace core.Entities.Orders;

public class OrderItem : BaseEntity
{
    public ProductOrdered ProductOrdered { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal LineSum { get; set; }
}

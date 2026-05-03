using System.ComponentModel.DataAnnotations.Schema;

namespace core.Entities.Orders;

public class Order : BaseEntity
{
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public required string CustomerId { get; set; }
    public required string StoreName { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];
    public Customer Customer { get; set; } = default!;
}

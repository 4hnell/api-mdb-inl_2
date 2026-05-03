namespace core.Entities;

public class Product : BaseEntity
{
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Weight { get; set; }
    public int PackSize { get; set; }
    public DateOnly BestBefore { get; set; }
    public DateOnly BakedOn { get; set; }
}
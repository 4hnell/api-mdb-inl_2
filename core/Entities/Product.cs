namespace core.Entities;

public class Product : BaseEntity
{
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Weight { get; set; }
    public int PackSize { get; set; }
    public DateTime BestBefore { get; set; }
    public DateTime BakedOn { get; set; }
}

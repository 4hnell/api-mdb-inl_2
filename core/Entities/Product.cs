namespace core.Entities;

public class Product : BaseEntity
{
    public required string ItemNumber { get; set; }
    public required string ProductName { get; set; }
    public List<ProductSupplier> ProductSuppliers { get; set; } = [];
}

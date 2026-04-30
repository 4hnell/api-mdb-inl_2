namespace core.Entities;

public class ProductSupplier : BaseEntity
{
    public required string ProductId { get; set; }
    public required string SupplierId { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; } = default!;
    public Supplier Supplier { get; set; } = default!;
}

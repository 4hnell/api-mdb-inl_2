namespace core.Entities;

public class ProductSupplier
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; } = default!;
    public Supplier Supplier { get; set; } = default!;
}

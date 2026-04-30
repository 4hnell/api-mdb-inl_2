namespace api.DTOs.Suppliers;

public class PostSupplierProductDto
{
    public required string ProductId { get; set; }
    public required string SupplierId { get; set; }
    public decimal Price { get; set; }
}

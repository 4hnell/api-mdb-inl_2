namespace api.DTOs.Products;

public class PostProductSupplierDto : PostProductDto
{
    public required string SupplierId { get; set; }
    public decimal Price { get; set; }
}

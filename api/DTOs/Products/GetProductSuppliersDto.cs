namespace api.DTOs.Products;

public class GetProductSuppliersDto
{
    public required string SupplierName { get; set; }
    public decimal Price { get; set; }
}

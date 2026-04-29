namespace api.DTOs.Suppliers;

public class GetProductsSupplierDto
{
    public required string ItemNumber { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
}

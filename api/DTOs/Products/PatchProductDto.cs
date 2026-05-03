namespace api.DTOs.Products;

public class PatchProductDto
{
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
}
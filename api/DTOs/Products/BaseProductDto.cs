namespace api.DTOs.Products;

public abstract class BaseProductDto
{
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Weight { get; set; }
    public int PackSize { get; set; }
}

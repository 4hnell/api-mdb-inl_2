namespace infrastructure.Data.SeedDTOs;

public class OrderItemSeedDto
{
    public required string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

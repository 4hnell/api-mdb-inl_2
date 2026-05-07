namespace infrastructure.Data.SeedDTOs;

public class OrderSeedDto
{
    public int OrderNumber { get; set; }
    public required string StoreName { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemSeedDto> OrderItems { get; set; } = [];
}

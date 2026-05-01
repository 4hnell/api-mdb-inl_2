namespace infrastructure.Data.SeedDTOs;

public class ProductSupplierSeedDto
{
    public required string ItemNumber { get; set; }
    public required string SupplierName { get; set; }
    public decimal Price { get; set; }
}

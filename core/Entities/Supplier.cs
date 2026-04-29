namespace core.Entities;

public class Supplier
{
    public int Id { get; set; }
    public required string SupplierName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public string? AddressLine { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Contact { get; set; }
    public List<ProductSupplier> ProductSuppliers { get; set; } = [];
}

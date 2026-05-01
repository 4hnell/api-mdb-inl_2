namespace core.Entities;

public class Supplier : BaseEntity
{
    public required string SupplierName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public string? AddressLine { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Contact { get; set; }
    public List<IngredientSupplier> IngredientSuppliers { get; set; } = [];
}

namespace api.DTOs.Suppliers;

public abstract class BaseSupplierDto
{
    public required string SupplierName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
}

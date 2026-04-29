namespace api.DTOs.Suppliers;

public class GetSupplierDto : BaseSupplierDto
{
    public string? AddressLine { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Contact { get; set; }
}

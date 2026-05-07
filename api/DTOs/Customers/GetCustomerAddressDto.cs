namespace api.DTOs.Customers;

public class GetCustomerAddressDto
{
    public required string AddressLine { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
}

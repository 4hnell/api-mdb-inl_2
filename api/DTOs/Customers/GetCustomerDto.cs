namespace api.DTOs.Customers;

public class GetCustomerDto : BaseCustomerDto
{
    public string? Contact { get; set; }
    public GetCustomerAddressDto? DeliveryAddress { get; set; }
    public GetCustomerAddressDto? BillingAddress { get; set; }
}

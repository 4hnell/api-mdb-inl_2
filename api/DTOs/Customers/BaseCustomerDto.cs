namespace api.DTOs.Customers;

public abstract class BaseCustomerDto
{
    public required string StoreName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
}

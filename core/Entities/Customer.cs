namespace core.Entities;

public class Customer : BaseEntity
{
    public required string StoreName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public string? Contact { get; set; }
    public Address? DeliveryAddress { get; set; }
    public Address? BillingAddress { get; set; }
}
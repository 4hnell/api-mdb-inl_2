namespace api.DTOs.Orders;

public class GetOrdersForCustomerDto : BaseOrderDto
{
    public required string Id { get; set; }
}
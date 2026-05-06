namespace api.DTOs.Orders;

public class GetAllOrdersDto : GetOrdersForCustomerDto
{
    public required string StoreName { get; set; }
}

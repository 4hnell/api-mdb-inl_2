using api.DTOs.Customers;

namespace api.DTOs.Orders;

public class GetOrderDto : BaseOrderDto
{
    public List<GetOrderItemDto>? OrderItems { get; set; }
    public GetCustomerDto? Customer { get; set; }
}

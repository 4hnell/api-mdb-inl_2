namespace api.DTOs.Orders;

public class GetOrderDto : BaseOrderDto
{
    public List<GetOrderItemDto>? OrderItems { get; set; }
}

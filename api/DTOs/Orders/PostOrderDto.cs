namespace api.DTOs.Orders;

public class PostOrderDto
{
    public required string StoreName { get; set; }
    public List<PostOrderItemDto>? OrderItems { get; set; }
}

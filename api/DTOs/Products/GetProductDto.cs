namespace api.DTOs.Products;

public class GetProductDto : BaseProductDto
{
    public DateOnly BestBefore { get; set; }
    public DateOnly BakedOn { get; set; }
}

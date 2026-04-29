namespace api.DTOs.Products;

public class GetProductDto : BaseProductDto
{
    public List<GetProductSuppliersDto>? Suppliers { get; set; }
}

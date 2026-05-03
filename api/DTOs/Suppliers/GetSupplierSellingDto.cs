namespace api.DTOs.Suppliers;

public class GetSupplierSellingDto : BaseSupplierDto
{
    public List<GetIngredientsSupplierDto>? Ingredients { get; set; }
}

namespace api.DTOs.Suppliers;

public class GetSupplierSearchDto : BaseSupplierDto
{
    public List<GetIngredientsSupplierDto>? Ingredients { get; set; }
}

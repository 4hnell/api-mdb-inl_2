namespace api.DTOs.Suppliers;

public class GetSupplierSearchDto : BaseSupplierDto
{
    public List<GetProductsSupplierDto>? Products { get; set; }
}

using api.DTOs.Customers;

namespace api.DTOs.Products;

public class GetProductBuyersDto : GetAllProductsDto
{
    public List<GetAllCustomersDto>? Buyers { get; set; }
}

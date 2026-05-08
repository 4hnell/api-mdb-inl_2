using api.DTOs.Customers;

namespace api.DTOs.Products;

public class GetProductBuyersDto : BaseProductDto
{
    public List<GetCustomersForProductDto>? Buyers { get; set; }
}

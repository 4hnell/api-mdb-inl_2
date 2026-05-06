using api.DTOs.Orders;

namespace api.DTOs.Customers;

public class GetCustomerOrdersDto : GetAllCustomersDto
{
    public IReadOnlyList<GetAllOrdersDto>? Orders { get; set; }
}

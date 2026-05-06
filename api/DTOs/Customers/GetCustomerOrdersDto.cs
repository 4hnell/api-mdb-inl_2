using api.DTOs.Orders;

namespace api.DTOs.Customers;

public class GetCustomerOrdersDto : BaseCustomerDto
{
    public List<GetOrdersForCustomerDto>? Orders { get; set; }
}

using core.Entities.Orders;

namespace core.Specifications;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(OrderSpecificationParams args) : base(c =>
        (string.IsNullOrWhiteSpace(args.Search) || c.StoreName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.StoreName) || (c.StoreName.ToLower().Trim() == args.StoreName.ToLower().Trim())) &&
        (!args.OrderNumber.HasValue || (c.OrderNumber == args.OrderNumber)) &&
        (!args.Date.HasValue || DateOnly.FromDateTime(c.OrderDate) == args.Date.Value))
    {
        AddInclude("OrderItems");
        AddInclude("Customer");
    }

    public OrderSpecification(string? orderId = null, int? orderNumber = null, bool latestOnly = false) : base(c =>
        (string.IsNullOrWhiteSpace(orderId) || c.Id == orderId) &&
        (!orderNumber.HasValue || c.OrderNumber == orderNumber))
    {
        AddInclude("OrderItems");
        AddInclude("Customer");

        if (latestOnly)
        {
            AddOrderByDescending(c => c.OrderNumber);
        }
    }
}

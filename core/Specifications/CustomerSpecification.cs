using core.Entities;

namespace core.Specifications;

public class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification(CustomerSpecificationParams args) : base(c =>
        (string.IsNullOrWhiteSpace(args.Search) || c.StoreName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.StoreName) || (c.StoreName.ToLower().Trim() == args.StoreName.ToLower().Trim())))
    {
        AddInclude("DeliveryAddress");
        AddInclude("BillingAddress");
    }

    public CustomerSpecification(string? customerId = null, string? storeName = null) : base(c =>
        (string.IsNullOrWhiteSpace(customerId) || c.Id == customerId) &&
        (string.IsNullOrWhiteSpace(storeName) || c.StoreName.ToLower().Trim() == storeName.ToLower().Trim()))
    {
        AddInclude("DeliveryAddress");
        AddInclude("BillingAddress");
    }
}

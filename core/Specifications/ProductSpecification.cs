using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams args) : base(c =>
        (string.IsNullOrWhiteSpace(args.Search) || c.ProductName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ProductName) || (c.ProductName.ToLower().Trim() == args.ProductName.ToLower().Trim())))
    {
        AddOrderByAscending(c => c.UnitPrice);
    }

    public ProductSpecification(string? productId = null, string? productName = null) : base(c =>
        (string.IsNullOrWhiteSpace(productId) || c.Id == productId) &&
        (string.IsNullOrWhiteSpace(productName) || c.ProductName.ToLower().Trim() == productName.ToLower().Trim()))
    {
        AddOrderByAscending(c => c.UnitPrice);
    }
}

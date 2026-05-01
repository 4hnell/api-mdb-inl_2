using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams args) : base(c =>
        (string.IsNullOrWhiteSpace(args.Search) || c.ProductName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ItemNumber) || (c.ItemNumber == args.ItemNumber)))
    {
        AddInclude("ProductSuppliers.Suppliers");
    }

    public ProductSpecification(string? productId = null, string? itemNumber = null) : base(c =>
        (string.IsNullOrWhiteSpace(productId) || c.Id == productId) &&
        (string.IsNullOrWhiteSpace(itemNumber) || c.ItemNumber.ToLower().Trim() == itemNumber.ToLower().Trim()))
    {
        AddInclude("ProductSuppliers.Suppliers");
    }
}

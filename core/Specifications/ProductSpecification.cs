using core.Entities;

namespace core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams args) : base(c =>
        (string.IsNullOrEmpty(args.Search) || c.ProductName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ItemNumber) || (c.ItemNumber == args.ItemNumber)))
    {
        AddInclude("ProductSuppliers.Suppliers");
    }
}

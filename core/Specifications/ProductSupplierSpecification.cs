using core.Entities;

namespace core.Specifications;

public class ProductSupplierSpecification : BaseSpecification<ProductSupplier>
{
    public ProductSupplierSpecification(ProductSupplierSpecificationParams args) : base(c =>
        string.IsNullOrWhiteSpace(args.ProductId) || c.ProductId == args.ProductId &&
        string.IsNullOrWhiteSpace(args.SupplierId) || c.SupplierId == args.SupplierId)
    { }

    public ProductSupplierSpecification(string supplierId, string productId) : base(c =>
        c.ProductId == productId && c.SupplierId == supplierId)
    { }
}

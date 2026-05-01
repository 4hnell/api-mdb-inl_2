using core.Entities;

namespace core.Specifications;

public class SupplierSpecification : BaseSpecification<Supplier>
{
    public SupplierSpecification(SupplierSpecificationParams args) : base(c =>
        string.IsNullOrWhiteSpace(args.Search) || c.SupplierName.ToLower().Contains(args.Search.ToLower()))
    {
        AddInclude("ProductSuppliers.Products");
    }

    public SupplierSpecification(string supplierId) : base(c =>
        c.Id == supplierId)
    {
        AddInclude("ProductSuppliers.Products");
    }
}

using core.Entities;

namespace core.Specifications;

public class SupplierSpecification : BaseSpecification<Supplier>
{
    public SupplierSpecification(SupplierSpecificationParams args) : base(c =>
        string.IsNullOrEmpty(args.Search) || c.SupplierName.ToLower().Contains(args.Search.ToLower()))
    {
        AddInclude("ProductSuppliers.Products");
    }
}

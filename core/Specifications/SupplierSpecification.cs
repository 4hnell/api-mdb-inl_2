using core.Entities;

namespace core.Specifications;

public class SupplierSpecification : BaseSpecification<Supplier>
{
    public SupplierSpecification(SupplierSpecificationParams args) : base(c =>
        (string.IsNullOrWhiteSpace(args.Search) || c.SupplierName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.SupplierName) || (c.SupplierName.ToLower().Trim() == args.SupplierName.ToLower().Trim())))
    {
        AddInclude("IngredientSuppliers.Ingredient");

        AddOrderByAscending(c => c.SupplierName);
    }

    public SupplierSpecification(string? supplierId = null, string? supplierName = null) : base(c =>
        (string.IsNullOrWhiteSpace(supplierId) || c.Id == supplierId) &&
        (string.IsNullOrWhiteSpace(supplierName) || c.SupplierName.ToLower().Trim() == supplierName.ToLower().Trim()))
    {
        AddInclude("IngredientSuppliers.Ingredient");

        AddOrderByAscending(c => c.SupplierName);
    }
}

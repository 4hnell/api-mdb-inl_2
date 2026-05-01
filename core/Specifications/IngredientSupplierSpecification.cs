using core.Entities;

namespace core.Specifications;

public class IngredientSupplierSpecification : BaseSpecification<IngredientSupplier>
{
    public IngredientSupplierSpecification(IngredientSupplierSpecificationParams args) : base(c =>
        string.IsNullOrWhiteSpace(args.IngredientId) || c.IngredientId == args.IngredientId &&
        string.IsNullOrWhiteSpace(args.SupplierId) || c.SupplierId == args.SupplierId)
    { }

    public IngredientSupplierSpecification(string ingredientId, string supplierId) : base(c =>
        c.IngredientId == ingredientId && c.SupplierId == supplierId)
    { }
}

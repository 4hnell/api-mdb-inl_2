using core.Entities;

namespace core.Specifications;

public class IngredientSpecification : BaseSpecification<Ingredient>
{
    public IngredientSpecification(IngredientSpecificationParams args) : base(c =>
        (string.IsNullOrWhiteSpace(args.Search) || c.IngredientName.ToLower().Contains(args.Search.ToLower())) &&
        (string.IsNullOrWhiteSpace(args.ItemNumber) || (c.ItemNumber == args.ItemNumber)))
    {
        AddInclude("IngredientSuppliers.Supplier");

        AddOrderByAscending(c => c.ItemNumber);
    }

    public IngredientSpecification(string? ingredientId = null, string? itemNumber = null) : base(c =>
        (string.IsNullOrWhiteSpace(ingredientId) || c.Id == ingredientId) &&
        (string.IsNullOrWhiteSpace(itemNumber) || c.ItemNumber.ToLower().Trim() == itemNumber.ToLower().Trim()))
    {
        AddInclude("IngredientSuppliers.Supplier");

        AddOrderByAscending(c => c.ItemNumber);
    }
}

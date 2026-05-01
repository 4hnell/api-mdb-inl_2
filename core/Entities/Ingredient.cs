namespace core.Entities;

public class Ingredient : BaseEntity
{
    public required string ItemNumber { get; set; }
    public required string IngredientName { get; set; }
    public List<IngredientSupplier> IngredientSuppliers { get; set; } = [];
}

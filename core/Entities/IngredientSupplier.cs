namespace core.Entities;

public class IngredientSupplier : BaseEntity
{
    public required string IngredientId { get; set; }
    public required string SupplierId { get; set; }
    public decimal Price { get; set; }
    public Ingredient Ingredient { get; set; } = default!;
    public Supplier Supplier { get; set; } = default!;
}

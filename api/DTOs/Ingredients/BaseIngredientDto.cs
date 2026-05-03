namespace api.DTOs.Ingredients;

public abstract class BaseIngredientDto
{
    public required string ItemNumber { get; set; }
    public required string IngredientName { get; set; }
}

namespace api.DTOs.Ingredients;

public class PostIngredientSupplierDto : PostIngredientDto
{
    public required string SupplierId { get; set; }
    public decimal Price { get; set; }
}

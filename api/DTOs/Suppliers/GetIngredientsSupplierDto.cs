namespace api.DTOs.Suppliers;

public class GetIngredientsSupplierDto
{
    public required string ItemNumber { get; set; }
    public required string IngredientName { get; set; }
    public decimal Price { get; set; }
}

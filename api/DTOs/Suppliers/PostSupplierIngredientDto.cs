namespace api.DTOs.Suppliers;

public class PostSupplierIngredientDto
{
    public required string IngredientId { get; set; }
    public required string SupplierId { get; set; }
    public decimal Price { get; set; }
}

namespace api.DTOs.Ingredients;

public class GetIngredientDto : BaseIngredientDto
{
    public List<GetIngredientSuppliersDto>? Suppliers { get; set; }
}

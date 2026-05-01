using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Ingredients;

public abstract class BaseIngredientDto
{
    [Required(ErrorMessage = "ItemNumber required")]
    public required string ItemNumber { get; set; }
    [Required(ErrorMessage = "IngredientName required")]
    public required string IngredientName { get; set; }
}

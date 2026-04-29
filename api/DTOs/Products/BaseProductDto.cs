using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Products;

public abstract class BaseProductDto
{
    [Required(ErrorMessage = "ItemNumber required")]
    public required string ItemNumber { get; set; }
    [Required(ErrorMessage = "ProductName required")]
    public required string ProductName { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Products;

public abstract class BaseProductDto
{
    [Required(ErrorMessage = "ProductName required")]
    public required string ProductName { get; set; }
    [Required(ErrorMessage = "UnitPrice required")]
    public decimal UnitPrice { get; set; }
    [Required(ErrorMessage = "Weight required")]
    public int Weight { get; set; }
    [Required(ErrorMessage = "PackSize required")]
    public int PackSize { get; set; }
}

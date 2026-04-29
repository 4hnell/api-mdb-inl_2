using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Suppliers;

public abstract class BaseSupplierDto
{
    [Required(ErrorMessage = "SupplierName required")]
    public required string SupplierName { get; set; }
    [Required(ErrorMessage = "Phone required")]
    public required string Phone { get; set; }
    [Required(ErrorMessage = "Email required")]
    public required string Email { get; set; }
}

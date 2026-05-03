using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Customers;

public abstract class BaseCustomerDto
{
    [Required(ErrorMessage = "StoreName required")]
    public required string StoreName { get; set; }
    [Required(ErrorMessage = "Phone required")]
    public required string Phone { get; set; }
    [Required(ErrorMessage = "Email required")]
    public required string Email { get; set; }
}

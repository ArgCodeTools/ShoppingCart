using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Application.DTOs.Requests;

public class CreateCartRequest
{
    [Required(ErrorMessage = "El DNI del usuario es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "El DNI debe ser mayor a 0")]
    public double UserDni { get; set; }
}
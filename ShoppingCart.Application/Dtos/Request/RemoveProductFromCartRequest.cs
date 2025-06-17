using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Application.DTOs.Requests;

public class RemoveProductFromCartRequest
{
    [Required(ErrorMessage = "El ID del carrito es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del carrito debe ser mayor a 0")]
    public int CartId { get; set; }

    [Required(ErrorMessage = "el ID del producto es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser mayor a 0")]
    public int ProductId { get; set; }
}

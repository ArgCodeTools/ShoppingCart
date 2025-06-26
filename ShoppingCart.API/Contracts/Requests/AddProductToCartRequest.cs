using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.API.Contracts.Requests;

public class AddProductToCartRequest
{
    [Required(ErrorMessage = "El ID del producto es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser mayor a 0")]
    public int ProductId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Contracts.Requests;

public class AddProductToCartRequest
{
    [Required(ErrorMessage = "El ID del producto es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser mayor a 0")]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Quantity { get; set; } = 1;
}
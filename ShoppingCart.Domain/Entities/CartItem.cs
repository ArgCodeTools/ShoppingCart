namespace ShoppingCart.Domain.Entities;

/// <summary>
/// Item el carrito.
/// </summary>
public class CartItem
{
    public required Product Product { get; set; }
    public required int Quantity { get; set; }
    public decimal Subtotal => Product.Price * Quantity;
}

namespace ShoppingCart.Domain.Entities;

/// <summary>
/// Item el carrito.
/// </summary>
public class CartItem
{
    public required Product Product { get; set; }

    public int Quantity { get; set; } = 1;

    public decimal Subtotal => Product.Price * Quantity;
}

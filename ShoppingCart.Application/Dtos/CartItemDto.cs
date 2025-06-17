namespace ShoppingCart.Application.DTOs;

public class CartItemDto
{
    public required ProductDto Product { get; set; }

    public int Quantity { get; set; }

    public decimal Subtotal { get; set; }
}

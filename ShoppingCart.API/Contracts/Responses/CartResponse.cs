namespace ShoppingCart.API.Contracts.Responses;

public class CartResponse
{
    public int Id { get; set; }
    public long UserDni { get; set; }
    public IEnumerable<CartItemResponse> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
}

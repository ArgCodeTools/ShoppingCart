namespace ShoppingCart.Application.DTOs;

public class CartDto
{
    public int Id { get; set; }
    public long UserDni { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public int TotalItems { get; set; }
    public decimal Total { get; set; }    
}
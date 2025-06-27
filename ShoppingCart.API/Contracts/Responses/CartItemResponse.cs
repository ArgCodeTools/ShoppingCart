namespace ShoppingCart.API.Contracts.Responses;

public class CartItemResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}

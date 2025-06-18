namespace ShoppingCart.Application.DTOs.Inputs;

public class AddProductToCartInput
{
    public int CartId { get; set; }    
    public int ProductId { get; set; }
}
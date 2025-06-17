namespace ShoppingCart.Application.DTOs.Inputs;

public class RemoveProductFromCartInput
{
    public int CartId { get; set; }    
    public int ProductId { get; set; }
}

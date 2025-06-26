namespace ShoppingCart.Infrastructure.DBModels;

public class ProductDbResult
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}

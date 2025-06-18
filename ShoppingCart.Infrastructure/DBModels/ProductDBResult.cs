namespace ShoppingCart.Infrastructure.DBModels;

public class ProductDBResult
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}

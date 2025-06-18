namespace ShoppingCart.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required double Dni { get; set; }
    public required string Name { get; set; }
    public required bool IsVip { get; set; }
}
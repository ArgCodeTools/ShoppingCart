namespace ShoppingCart.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public double Dni { get; set; }
    public required string Name { get; set; }
    public bool IsVip { get; set; }
}
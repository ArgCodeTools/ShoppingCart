namespace ShoppingCart.Domain.Entities;

/// <summary>
/// Productos que se pueden agregar al carrito.
/// </summary>
public class Product
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
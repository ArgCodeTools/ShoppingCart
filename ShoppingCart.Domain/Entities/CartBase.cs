using ShoppingCart.Domain.Enums;

namespace ShoppingCart.Domain.Entities;

/// <summary>
/// Clase base para el carrito de compras. Contiene la lógica común para agregar, eliminar y actualizar items.
/// </summary>
public abstract class CartBase
{
    protected List<CartItem> _items = new List<CartItem>();

    /// <summary>
    /// Identificador del carrito.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Usuario al que pertences el carrito.
    /// </summary>
    public required User User { get; set; }

    /// <summary>
    /// La lista de items no se puede eliminar.
    /// </summary>
    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Indica el tipo de carrito.
    /// </summary>
    public abstract CartType Type { get; }

    /// <summary>
    /// Agrega un item al carrito.
    /// </summary>
    public virtual void AddItem(Product product)
    {
        if (_items.Any(x => x.Product.Id == product.Id))
            throw new InvalidOperationException($"El producto {product.Id} ya está en el carrito.");

        _items.Add(new CartItem { Product = product });
    }

    /// <summary>
    /// Elimina un item del carrito.
    /// </summary>
    public virtual bool RemoveItem(Product product)
    {
        var item = _items.FirstOrDefault(x => x.Product.Id == product.Id);
        if (item != null)
        {
            return _items.Remove(item);
        }
        return false;
    }

    public virtual decimal CalculateTotal()
    {
        var itemCount = _items.Count;
        var subtotal = _items.Sum(x => x.Subtotal);

        // Si tiene exactamente 5 productos, descuento del 20%
        if (itemCount == 5)
        {
            return subtotal * 0.8m; // 20% de descuento
        }

        // Si tiene más de 10 productos, aplicar descuentos específicos por tipo de carrito
        if (itemCount > 10)
        {
            return CalculateTotalWithDiscount(subtotal);
        }

        // Cálculo normal (menos de 10 productos y distinto de 5)
        return subtotal;
    }

    protected abstract decimal CalculateTotalWithDiscount(decimal subtotal);    
}

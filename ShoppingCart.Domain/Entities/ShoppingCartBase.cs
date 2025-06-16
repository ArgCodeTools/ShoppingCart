namespace ShoppingCart.Domain.Entities;

/// <summary>
/// Clase base para el carrito de compras. Contiene la lógica común para agregar, eliminar y actualizar items.
/// </summary>
public abstract class ShoppingCartBase
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
    /// Agrega un item con su cantidad al carrito.
    /// </summary>
    public virtual void AddItem(Product product, int quantity)
    {
        var existingItem = _items.FirstOrDefault(x => x.Product.Id == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem() { Product = product, Quantity = quantity });            
        }
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

    /// <summary>
    /// Actualiza la cantidad de un item en el carrito.
    /// </summary>
    public virtual bool UpdateItemQuantity(Product product, int quantity)
    {
        var item = _items.FirstOrDefault(x => x.Product.Id == product.Id);

        if (item != null)
        {
            if (quantity <= 0)
            {
                return RemoveItem(product);
            }
            item.Quantity = quantity;
            return true;
        }
        return false;
    }

    public virtual decimal CalculateTotal()
    {
        var itemCount = _items.Sum(x => x.Quantity);
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

    public int GetTotalItemCount() => _items.Sum(x => x.Quantity);

    public bool IsEmpty() => !_items.Any();

    public void ClearCart() => _items.Clear();
}

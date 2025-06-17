namespace ShoppingCart.Domain.Entities.Carts;

public class SpecialDateCart : CartBase
{
    protected override decimal CalculateTotalWithDiscount(decimal subtotal)
    {
        // Carrito Promocionable por Fecha Especial: Descuento de $500 si tiene más de 10 productos
        return subtotal - 500m;
    }
}

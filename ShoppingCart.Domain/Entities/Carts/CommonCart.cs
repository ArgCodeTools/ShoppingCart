namespace ShoppingCart.Domain.Entities.Carts;

public class CommonCart : CartBase
{
    protected override decimal CalculateTotalWithDiscount(decimal subtotal)
    {
        if (subtotal < 200m)
        {
            // Si el subtotal es menor a $200, no se aplica descuento
            return subtotal;
        }

        // Carrito Común: Descuento de $200 si tiene más de 10 productos
        return subtotal - 200m;
    }
}
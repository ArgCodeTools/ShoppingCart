using ShoppingCart.Domain.Enums;

namespace ShoppingCart.Domain.Entities.Carts;

public class SpecialDateCart : CartBase
{
    public override CartType Type => CartType.SpecialDate;

    protected override decimal CalculateTotalWithDiscount(decimal subtotal)
    {
        // Carrito Promocionable por Fecha Especial: Descuento de $500 si tiene más de 10 productos
        return subtotal - 500m;
    }
}

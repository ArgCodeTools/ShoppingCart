using ShoppingCart.Domain.Enums;

namespace ShoppingCart.Domain.Entities.Carts;

public class VipCart : CartBase
{
    public override CartType Type => CartType.Vip;

    protected override decimal CalculateTotalWithDiscount(decimal subtotal)
    {
        // Carrito VIP: Bonificación del producto más barato y descuento general de $700
        var cheapestItem = _items.OrderBy(x => x.Product.Price).FirstOrDefault();
        var cheapestItemDiscount = cheapestItem?.Product.Price ?? 0;

        return subtotal - cheapestItemDiscount - 700m;
    }
}

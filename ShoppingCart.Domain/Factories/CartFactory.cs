using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Entities.Carts;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Domain.Factories;

public class CartFactory : ICartFactory
{
    /// <summary>
    /// Crea un carrito según las reglas de negocio:
    /// 1. Si el usuario es VIP -> VipCart (prioridad)
    /// 2. Si no es VIP pero estamos en fecha especial -> SpecialDateCart
    /// 3. En caso contrario -> CommonCart
    /// El ID será asignado por la base de datos al persistir.
    /// </summary>
    public CartBase CreateCart(User user, bool isSpecialDate)
    {
        // Prioridad 1: Usuario VIP
        if (user.IsVip)
        {
            return new VipCart { User = user };
        }

        // Prioridad 2: Fecha especial
        if (isSpecialDate)
        {
            return new SpecialDateCart { User = user };
        }

        // Por defecto: Carrito común
        return new CommonCart { User = user };
    }
}
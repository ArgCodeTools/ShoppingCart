using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Entities.Carts;
using ShoppingCart.Domain.Enums;
namespace ShoppingCart.Domain.Factories;

public class ShoppingCartFactory : IShoppingCartFactory
{
    /// <summary>
    /// Crea un carrito según las reglas de negocio:
    /// 1. Si el usuario es VIP -> VipCart (prioridad)
    /// 2. Si no es VIP pero estamos en fecha especial -> SpecialDateCart
    /// 3. En caso contrario -> CommonCart
    /// El ID será asignado por la base de datos al persistir.
    /// </summary>
    public ShoppingCartBase CreateCart(User user, bool isSpecialDate)
    {
        var cartType = DetermineCartType(user, isSpecialDate);

        return cartType switch
        {
            CartType.Vip => new VipCart { User = user },
            CartType.SpecialDate => new SpecialDateCart { User = user },
            CartType.Common => new CommonCart { User = user },
            _ => throw new ArgumentException($"Tipo de carrito no válido: {cartType}")
        };
    }

    /// <summary>
    /// Determina el tipo de carrito según las reglas de promoción.
    /// Las promociones no se combinan entre sí.
    /// Método privado - lógica interna del factory.
    /// </summary>
    private CartType DetermineCartType(User user, bool isSpecialDate)
    {
        // Prioridad 1: Usuario VIP
        if (user.IsVip)
        {
            return CartType.Vip;
        }

        // Prioridad 2: Fecha especial
        if (isSpecialDate)
        {
            return CartType.SpecialDate;
        }

        // Por defecto: Carrito común
        return CartType.Common;
    }
}

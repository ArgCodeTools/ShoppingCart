using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces;

/// <summary>
/// Factory para crear carritos según el tipo de usuario y fechas especiales.
/// </summary>
public interface IShoppingCartFactory
{
    ShoppingCartBase CreateCart(User user, bool isSpecialDate);
}
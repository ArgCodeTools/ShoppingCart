using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces;

/// <summary>
/// Factory para crear carritos según el tipo de usuario y fechas especiales.
/// </summary>
public interface ICartFactory
{
    CartBase CreateCart(User user, bool isSpecialDate);
}
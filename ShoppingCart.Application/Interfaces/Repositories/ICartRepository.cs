using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<CartBase> CreateAsync(CartBase cart);
    Task<CartBase?> GetByIdAsync(int cartId);
    Task<bool> DeleteAsync(int cartId);
    Task<CartBase> UpdateAsync(CartBase cart);
    Task<bool> ExistsAsync(int cartId);
}
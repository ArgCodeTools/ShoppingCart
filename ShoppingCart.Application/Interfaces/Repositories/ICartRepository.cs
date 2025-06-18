using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Interfaces.Repositories;

public interface ICartRepository
{
    Task<int> CreateAsync(CartBase cart);
    Task<CartBase?> GetByIdAsync(int cartId);
    Task DeleteAsync(int cartId);
    Task AddItemAsync(int cartId, int productId);
    Task DeleteItemAsync(int cartId, int productId);
    Task<bool> ExistsAsync(int cartId);
}
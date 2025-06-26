using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.DBModels;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface ICartMapper
{
    Task<CartDbResult?> GetByIdAsync(int cartId);
    Task<IEnumerable<ProductDbResult>> GetProductsByCartIdAsync(int cartId);
    Task<bool> ExistsAsync(int cartId);
    Task DeleteAsync(int cartId);
    Task<int> CreateAsync(CartBase cart);
    Task AddItemAsync(int cartId, int productId);
    Task DeleteItemAsync(int cartId, int productId);
}
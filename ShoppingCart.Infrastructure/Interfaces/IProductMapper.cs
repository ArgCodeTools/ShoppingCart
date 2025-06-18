using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IProductMapper
{
    Task<bool> ExistsAsync(int productId);

    Task<IEnumerable<Product>> GetMostExpensiveByUserAsync(long userDni);

    Task<Product?> GetProductByIdAsync(int productId);
}

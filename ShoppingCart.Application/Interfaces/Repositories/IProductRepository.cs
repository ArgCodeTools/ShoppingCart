using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId);

    Task<bool> ExistsAsync(int productId);

    Task<IEnumerable<Product>> GetMostExpensiveByUserAsync(long userDni);
}
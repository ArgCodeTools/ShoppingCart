using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int productId);
    Task<List<Product>> GetByIdsAsync(List<int> productIds);
    Task<bool> ExistsAsync(int productId);
    Task<List<Product>> GetMostExpensiveByUserAsync(double userDni);
}
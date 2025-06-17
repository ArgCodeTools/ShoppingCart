using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public Task<bool> ExistsAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByIdAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetByIdsAsync(List<int> productIds)
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetMostExpensiveByUserAsync(double userDni)
    {
        throw new NotImplementedException();
    }
}

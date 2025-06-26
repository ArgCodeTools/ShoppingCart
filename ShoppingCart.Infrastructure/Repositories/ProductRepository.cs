using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class ProductRepository(IProductMapper productMapper) : IProductRepository
{
    public async Task<bool> ExistsAsync(int productId)
    {
        var result = await productMapper.ExistsAsync(productId);

        return result;
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        var result = await productMapper.GetProductByIdAsync(productId);

        return result;
    }

    public async Task<IEnumerable<Product>> GetMostExpensiveByUserAsync(long userDni)
    {
        var result = await productMapper.GetMostExpensiveByUserAsync(userDni);

        return result;
    }
}

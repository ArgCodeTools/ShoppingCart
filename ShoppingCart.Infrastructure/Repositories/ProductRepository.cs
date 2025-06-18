using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IProductMapper _productMapper;

    public ProductRepository(IProductMapper productMapper)
    {
        _productMapper = productMapper;
    }

    public async Task<bool> ExistsAsync(int productId)
    {
        var result = await _productMapper.ExistsAsync(productId);

        return result;
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        var result = await _productMapper.GetProductByIdAsync(productId);

        return result;
    }

    public async Task<IEnumerable<Product>> GetMostExpensiveByUserAsync(long userDni)
    {
        var result = await _productMapper.GetMostExpensiveByUserAsync(userDni);

        return result;
    }
}

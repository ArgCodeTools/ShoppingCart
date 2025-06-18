using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;
using System.Net;

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

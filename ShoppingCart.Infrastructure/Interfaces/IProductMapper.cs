using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IProductMapper
{
    Task<bool> ExistsAsync(int productId);
    Task<Product?> GetProductByIdAsync(int productId);
}

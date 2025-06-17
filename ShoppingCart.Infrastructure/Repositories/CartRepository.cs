using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    public Task<CartBase> CreateAsync(CartBase cart)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<CartBase?> GetByIdAsync(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<CartBase> UpdateAsync(CartBase cart)
    {
        throw new NotImplementedException();
    }
}

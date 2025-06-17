using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<bool> ExistsAsync(double dni)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByDniAsync(double dni)
    {
        throw new NotImplementedException();
    }
}

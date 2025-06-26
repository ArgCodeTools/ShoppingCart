using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class UserRepository(IUserMapper userMapper) : IUserRepository
{
    public async Task<bool> ExistsAsync(long dni)
    {
        var result = await userMapper.ExistsAsync(dni);

        return result;
    }

    public async Task<User?> GetByDniAsync(long dni)
    {
        var result = await userMapper.GetUserByDniAsync(dni);

        return result;
    }
}

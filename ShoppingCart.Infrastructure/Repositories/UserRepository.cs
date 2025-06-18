using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private IUserMapper _userMapper;

    public UserRepository(IUserMapper userMapper)
    {
        _userMapper = userMapper;
    }

    public async Task<bool> ExistsAsync(double dni)
    {
        var result = await _userMapper.ExistsAsync(dni);

        return result;
    }

    public async Task<User?> GetByDniAsync(double dni)
    {
        var result = await _userMapper.GetUserByDniAsync(dni);

        return result;
    }
}

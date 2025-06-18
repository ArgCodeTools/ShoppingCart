using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IUserMapper
{
    Task<User?> GetUserByDniAsync(long dni);
    Task<bool> ExistsAsync(long dni);
    Task<User?> GetUserByIdAsync(int userId);
}

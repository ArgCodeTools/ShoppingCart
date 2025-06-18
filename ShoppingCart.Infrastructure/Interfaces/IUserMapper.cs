using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface IUserMapper
{
    Task<User?> GetUserByDniAsync(double dni);
    Task<bool> ExistsAsync(double dni);
    Task<User?> GetUserByIdAsync(int userId);
}

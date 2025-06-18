using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByDniAsync(long dni);
    Task<bool> ExistsAsync(long dni);
}
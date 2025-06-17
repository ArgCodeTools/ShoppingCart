using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByDniAsync(double dni);
    Task<bool> ExistsAsync(double dni);
}
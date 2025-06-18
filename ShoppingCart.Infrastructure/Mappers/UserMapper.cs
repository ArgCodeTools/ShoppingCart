using Dapper;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Mappers;

public class UserMapper : IUserMapper
{
    private readonly ISqlExecutor _executor;

    public UserMapper(ISqlExecutor executor)
    {
        _executor = executor;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_UserId", userId);

        var user = await _executor.QueryFirstOrDefaultAsync<User>("SP_User_GetById", parameters);

        return user;
    }

    public async Task<bool> ExistsAsync(long dni)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Dni", dni);

        return await _executor.QueryFirstOrDefaultAsync<bool>("SP_User_Exists", parameters);
    }

    public async Task<User?> GetUserByDniAsync(long dni)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_UserId", dni);

        var user = await _executor.QueryFirstOrDefaultAsync<User>("SP_User_GetByDni", parameters);

        return user;
    }
}

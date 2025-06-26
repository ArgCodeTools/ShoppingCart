using Dapper;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Mappers;

public class UserMapper(ISqlExecutor executor) : IUserMapper
{
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Id", userId);

        var user = await executor.QueryFirstOrDefaultAsync<User>("SP_User_GetById", parameters);

        return user;
    }

    public async Task<bool> ExistsAsync(long dni)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Dni", dni);

        return await executor.QueryFirstOrDefaultAsync<bool>("SP_User_Exists", parameters);
    }

    public async Task<User?> GetUserByDniAsync(long dni)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Dni", dni);

        var user = await executor.QueryFirstOrDefaultAsync<User>("SP_User_GetByDni", parameters);

        return user;
    }
}

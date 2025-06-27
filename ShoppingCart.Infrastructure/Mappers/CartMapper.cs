using Dapper;
using ShoppingCart.Domain.Enums;
using ShoppingCart.Infrastructure.DBModels;
using ShoppingCart.Infrastructure.Interfaces;
using System.Data;

namespace ShoppingCart.Infrastructure.Mappers;

public class CartMapper(ISqlExecutor executor) : ICartMapper
{
    public async Task<CartDbResult?> GetByIdAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Id", cartId);

        var result = await executor.QueryFirstOrDefaultAsync<CartDbResult>("SP_Cart_GetById", parameters);        

        return result;
    }

    public async Task<IEnumerable<ProductDbResult>> GetProductsByCartIdAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);

        var products = await executor.QueryAsync<ProductDbResult>("SP_CartItem_GetByCartId", parameters);

        return products;
    }

    public async Task<bool> ExistsAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Id", cartId);

        return await executor.QueryFirstOrDefaultAsync<bool>("SP_Cart_Exists", parameters);
    }

    public async Task DeleteAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Id", cartId);

        await executor.ExecuteAsync("SP_Cart_Remove", parameters);
    }

    public async Task<int> CreateAsync(int userId, CartType cartType)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_UserId", userId);
        parameters.Add("p_CartTypeId", (int)cartType);
        parameters.Add("p_CartId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await executor.ExecuteAsync("SP_Cart_Insert", parameters);

        return parameters.Get<int>("p_CartId");
    }

    public async Task AddItemAsync(int cartId, int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);
        parameters.Add("p_ProductId", productId);

        await executor.ExecuteAsync("SP_CartItem_Add", parameters);
    }

    public async Task DeleteItemAsync(int cartId, int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);
        parameters.Add("p_ProductId", productId);

        await executor.ExecuteAsync("SP_CartItem_Remove", parameters);
    }
}

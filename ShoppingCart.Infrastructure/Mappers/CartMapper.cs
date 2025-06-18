using Dapper;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.DBModels;
using ShoppingCart.Infrastructure.Interfaces;
using System.Data;

namespace ShoppingCart.Infrastructure.Mappers;

internal class CartMapper : ICartMapper
{
    private readonly ISqlExecutor _executor;

    public CartMapper(ISqlExecutor executor)
    {
        _executor = executor;
    }

    public async Task<CartDbResult?> GetByIdAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);

        var result = await _executor.QueryFirstOrDefaultAsync<CartDbResult>("SP_Cart_GetById", parameters);        

        return result;
    }

    public async Task<IEnumerable<ProductDBResult>> GetProductsByCartIdAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);

        var products = await _executor.QueryAsync<ProductDBResult>("SP_Cart_GetItems", parameters);

        return products;
    }

    public async Task<bool> ExistsAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);

        return await _executor.QueryFirstOrDefaultAsync<bool>("SP_Cart_Exists", parameters);
    }

    public async Task DeleteAsync(int cartId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);

        await _executor.ExecuteAsync("SP_Cart_Remove", parameters);
    }

    public async Task<int> CreateAsync(CartBase cart)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_UserId", cart.User.Id);
        parameters.Add("p_CartTypeId", (int)cart.Type);
        parameters.Add("p_CartId", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await _executor.ExecuteAsync("SP_Cart_Insert", parameters);

        return parameters.Get<int>("p_CartId");
    }

    public async Task AddItemAsync(int cartId, int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);
        parameters.Add("p_ProductId", productId);

        await _executor.ExecuteAsync("SP_Cart_AddItem", parameters);
    }

    public async Task DeleteItemAsync(int cartId, int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_CartId", cartId);
        parameters.Add("p_ProductId", productId);

        await _executor.ExecuteAsync("SP_Cart_RemoveItem", parameters);
    }
}

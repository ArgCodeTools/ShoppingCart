using Dapper;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Mappers;

public class ProductMapper(ISqlExecutor executor) : IProductMapper
{
    public async Task<bool> ExistsAsync(int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Id", productId);

        return await executor.QueryFirstOrDefaultAsync<bool>("SP_Product_Exists", parameters);
    }

    public Task<IEnumerable<Product>> GetMostExpensiveByUserAsync(long userDni, int top)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_UserDni", userDni);
        parameters.Add("p_Top", top);

        var products = executor.QueryAsync<Product>("SP_Product_GetMostExpensiveByUser", parameters);

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_Id", productId);

        var product = await executor.QueryFirstOrDefaultAsync<Product>("SP_Product_GetById", parameters);
        return product;
    }
}

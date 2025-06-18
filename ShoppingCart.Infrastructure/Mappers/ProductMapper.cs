using Dapper;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Mappers;

public class ProductMapper : IProductMapper
{
    private readonly ISqlExecutor _executor;

    public ProductMapper(ISqlExecutor executor)
    {
        _executor = executor;
    }

    public async Task<bool> ExistsAsync(int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_ProductId", productId);

        return await _executor.QueryFirstOrDefaultAsync<bool>("SP_Product_Exists", parameters);
    }

    public Task<IEnumerable<Product>> GetMostExpensiveByUserAsync(long userDni)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_UserDni", userDni);
        parameters.Add("p_Top", 4); // TODO: This should be configurable

        var products = _executor.QueryAsync<Product>("SP_Product_GetMostExpensiveByUser", parameters);

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_ProductId", productId);

        var product = await _executor.QueryFirstOrDefaultAsync<Product>("SP_Product_GetById", parameters);
        return product;
    }
}

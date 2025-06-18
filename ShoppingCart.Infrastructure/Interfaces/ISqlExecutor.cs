using Dapper;
using System.Data;

namespace ShoppingCart.Infrastructure.Interfaces;

public interface ISqlExecutor
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.StoredProcedure);

    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.StoredProcedure);

    Task ExecuteAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);
}

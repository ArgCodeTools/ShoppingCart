using Dapper;
using ShoppingCart.Infrastructure.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ShoppingCart.Infrastructure.Services;

public class SqlExecutor : ISqlExecutor
{
    private readonly string _connectionString;

    public SqlExecutor(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(nameof(configuration));
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.StoredProcedure)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, parameters, commandType: commandType);
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.StoredProcedure)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType);
    }

    public async Task ExecuteAsync(string sql, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync(sql, parameters, commandType: commandType);
    }
}


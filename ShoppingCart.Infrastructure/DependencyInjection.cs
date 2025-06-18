using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Services;

namespace ShoppingCart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISqlExecutor, SqlExecutor>();        

        return services;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Mappers;
using ShoppingCart.Infrastructure.Configuration;
using ShoppingCart.Infrastructure.Repositories;
using ShoppingCart.Infrastructure.Services;

namespace ShoppingCart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SpecialDateConfiguration>(configuration.GetSection("SpecialDate"));
        services.AddScoped<ISpecialDateService, SpecialDateService>();
        
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        services.AddScoped<ISqlExecutor, SqlExecutor>();
        
        services.AddScoped<ICartMapper, CartMapper>();
        services.AddScoped<IProductMapper, ProductMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
        
        return services;
    }
}

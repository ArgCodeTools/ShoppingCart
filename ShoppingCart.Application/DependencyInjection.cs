using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Services;
using ShoppingCart.Domain.Factories;
using ShoppingCart.Domain.Interfaces;
using System.Reflection;

namespace ShoppingCart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar servicios de aplicación
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IEntityValidationService, EntityValidationService>();

        // Registrar factories del dominio
        services.AddScoped<ICartFactory, CartFactory>();

        // Registrar AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
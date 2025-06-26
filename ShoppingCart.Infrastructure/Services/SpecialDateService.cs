using Microsoft.Extensions.Options;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Infrastructure.Configuration;

namespace ShoppingCart.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de fechas especiales usando configuración de appsettings.
/// </summary>
public class SpecialDateService(IOptions<SpecialDateConfiguration> configuration) : ISpecialDateService
{
    private readonly SpecialDateConfiguration _configuration = configuration.Value;

    public bool IsSpecialDate()
    {
        return _configuration.IsSpecialDate;
    }
}
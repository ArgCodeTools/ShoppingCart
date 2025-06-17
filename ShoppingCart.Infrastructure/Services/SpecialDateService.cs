using Microsoft.Extensions.Options;
using ShoppingCart.Application.Services;
using ShoppingCart.Infrastructure.Models;

namespace ShoppingCart.Infrastructure.Services;

/// <summary>
/// Implementación del servicio de fechas especiales usando configuración de appsettings.
/// </summary>
public class SpecialDateService : ISpecialDateService
{
    private readonly SpecialDateConfiguration _configuration;

    public SpecialDateService(IOptions<SpecialDateConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public bool IsSpecialDate()
    {
        return _configuration.IsSpecialDate;
    }
}
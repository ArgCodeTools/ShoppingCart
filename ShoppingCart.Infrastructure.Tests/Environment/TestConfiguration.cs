using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Infrastructure.Tests.Environment;

public class TestConfiguration
{
    [Required, MinLength(1)]
    public string ConnectionString { get; set; } = default!;

    [Required, MinLength(1)]
    public string DatabaseName { get; set; } = default!;

    [Required, MinLength(1)]
    public string MigrationsPath { get; set; } = default!;

    public bool RecreateDatabase { get; set; }
}

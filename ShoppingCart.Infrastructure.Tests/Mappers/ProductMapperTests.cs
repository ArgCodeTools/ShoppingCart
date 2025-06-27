using ShoppingCart.Infrastructure.Mappers;
using ShoppingCart.Infrastructure.Tests.Environment;

namespace ShoppingCart.Infrastructure.Tests.Mappers;

public class ProductMapperTests : IClassFixture<TestDatabaseEnvironment>
{
    private ProductMapper _productMapper;

    public ProductMapperTests(TestDatabaseEnvironment environment)
    {
        _productMapper = new ProductMapper(environment.SqlExecutor);
    }

    [Fact]
    public async Task GetProductByIdAsync_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        int existentProductId = 1;

        // Act
        var product = await _productMapper.GetProductByIdAsync(existentProductId);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(existentProductId, product.Id);
        Assert.Equal("Remera negra", product.Name);
        Assert.Equal(10000.50m, product.Price);
    }

    [Fact]
    public async Task GetProductByIdAsync_WhenProductDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        int nonExistentProductId = 999;

        // Act
        var product = await _productMapper.GetProductByIdAsync(nonExistentProductId);

        // Assert
        Assert.Null(product);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingProductId_ShouldReturnTrue()
    {
        // Arrange
        var existingProductId = 1;

        // Act
        var exists = await _productMapper.ExistsAsync(existingProductId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingProductId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingProductId = 999;

        // Act
        var exists = await _productMapper.ExistsAsync(nonExistingProductId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task GetMostExpensiveByUserAsync_WithValidUserAndProducts_ShouldReturnProductsOrderedByPriceDescending()
    {
        // Arrange
        var userDni = 1122334455;
        var top = 4;

        // Act
        var products = await _productMapper.GetMostExpensiveByUserAsync(userDni, top);

        // Assert
        Assert.NotNull(products);
        Assert.Equal(top, products.Count());

        var values = products.ToList();

        Assert.Equal(6, values[0].Id);
        Assert.Equal(4, values[1].Id);
        Assert.Equal(3, values[2].Id);
        Assert.Equal(2, values[3].Id);
    }


}

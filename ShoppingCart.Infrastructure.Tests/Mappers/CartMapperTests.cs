using ShoppingCart.Domain.Enums;
using ShoppingCart.Infrastructure.Mappers;
using ShoppingCart.Infrastructure.Tests.Environment;

namespace ShoppingCart.Infrastructure.Tests.Mappers;

public class CartMapperTests : IClassFixture<TestDatabaseEnvironment>
{
    private CartMapper _cartMapper;

    public CartMapperTests(TestDatabaseEnvironment environment)
    {
        _cartMapper = new CartMapper(environment.SqlExecutor);
    }

    [Fact]
    public async Task GetCartByIdAsync_WhenCartExists_ShouldReturnCart()
    {
        // Arrange
        int existingCartId = 20;

        // Act
        var cart = await _cartMapper.GetByIdAsync(existingCartId);

        // Assert
        Assert.NotNull(cart);
        Assert.Equal(existingCartId, cart.Id);
        Assert.Equal(2, cart.UserId);
        Assert.Equal(1, cart.CartTypeId);
    }

    [Fact]
    public async Task GetCartByIdAsync_WhenCartDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        int nonExistingCartId = 999;

        // Act
        var cart = await _cartMapper.GetByIdAsync(nonExistingCartId);

        // Assert
        Assert.Null(cart);
    }

    [Fact]
    public async Task GetProductsByCartIdAsync_WhenCartHasProducts_ShouldReturnProducts()
    {
        // Arrange
        int cartId = 21;

        // Act
        var products = await _cartMapper.GetProductsByCartIdAsync(cartId);

        // Assert
        Assert.NotNull(products);
        Assert.Equal(3, products.Count());
        Assert.Contains(products, p => p.Id == 1);
        Assert.Contains(products, p => p.Id == 3);
        Assert.Contains(products, p => p.Id == 6);
    }

    [Fact]
    public async Task GetProductsByCartIdAsync_WhenCartHasNoProducts_ShouldReturnEmptyCollection()
    {
        // Arrange
        int cartId = 24;

        // Act
        var products = await _cartMapper.GetProductsByCartIdAsync(cartId);

        // Assert
        Assert.NotNull(products);
        Assert.Empty(products);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingCart_ShouldReturnTrue()
    {
        // Arrange
        var existingCartId = 20;

        // Act
        var exists = await _cartMapper.ExistsAsync(existingCartId);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingCart_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingCartId = 999;

        // Act
        var exists = await _cartMapper.ExistsAsync(nonExistingCartId);

        // Assert
        Assert.False(exists);
    }

    [Fact]
    public async Task DeleteAsync_WithExistingCart_ShouldDeleteCart()
    {
        // Arrange
        var cartIdToDelete = await _cartMapper.CreateAsync(userId: 2, CartType.Common);
        
        // Act
        await _cartMapper.DeleteAsync(cartIdToDelete);

        // Assert
        var exists = await _cartMapper.ExistsAsync(cartIdToDelete);

        Assert.False(exists);
    }

    [Fact]
    public async Task CreateAsync_WhenUserExists_ShouldInsertNewCart()
    {
        // Act
        var cartId = await _cartMapper.CreateAsync(userId: 2, CartType.Common);

        // Assert
        Assert.True(cartId > 0);
        var exists = await _cartMapper.ExistsAsync(cartId);
        Assert.True(exists);

        await _cartMapper.DeleteAsync(cartId);
    }

    [Fact]
    public async Task AddItemAsync_WhenCartExists_ShouldAddProductToCart()
    {
        // Arrange
        var productId = 1;
        var emptyCartId = 24;

        // Act
        await _cartMapper.AddItemAsync(emptyCartId, productId);

        // Assert
        var products = await _cartMapper.GetProductsByCartIdAsync(emptyCartId);
        Assert.Contains(products, p => p.Id == productId);

        await _cartMapper.DeleteItemAsync(emptyCartId, productId);
    }

    [Fact]
    public async Task DeleteItemAsync_WhenCartExists_ShouldRemoveProductFromCart()
    {
        // Arrange
        var productId = 1;
        var cartId = 23;
        await _cartMapper.AddItemAsync(cartId, productId);

        // Act
        await _cartMapper.DeleteItemAsync(cartId, productId);

        // Assert
        var products = await _cartMapper.GetProductsByCartIdAsync(cartId);
        Assert.DoesNotContain(products, p => p.Id == productId);
    }
}

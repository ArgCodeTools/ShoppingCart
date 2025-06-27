using Moq;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Entities.Carts;
using ShoppingCart.Domain.Enums;
using ShoppingCart.Infrastructure.DBModels;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Repositories;

namespace ShoppingCart.Infrastructure.Tests.Repositories;

public class CartRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_WhenCartExists_ShouldReturnCart()
    {
        // Arrange
        var cartMapperMock = CreateCartMapperMock();
        var userMapperMock = new Mock<IUserMapper>();
        var cartRepository = new CartRepository(cartMapperMock.Object, userMapperMock.Object);

        userMapperMock.Setup(m => m.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new User { Id = 11, Dni = 111223344, IsVip = true, Name = "User Test" });

        // Act
        var cart = await cartRepository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(cart);
        Assert.Equal(1, cart.Id);
        Assert.Equal(CartType.Common, cart.Type);

        Assert.Equal(11, cart.User.Id);
        Assert.Equal("User Test", cart.User.Name);
        Assert.Equal(111223344, cart.User.Dni);
        Assert.True(cart.User.IsVip);

        Assert.Single(cart.Items);
        Assert.Equal(101, cart.Items[0].Product.Id);
        Assert.Equal("product test 1", cart.Items[0].Product.Name);
        Assert.Equal(1234.50m, cart.Items[0].Product.Price);
    }

    private Mock<ICartMapper> CreateCartMapperMock()
    {
        var cartMapperMock = new Mock<ICartMapper>();

        var cartDbResults = new List<CartDbResult>
        {
            new CartDbResult { Id = 1, CartTypeId = (int)CartType.Common },
            new CartDbResult { Id = 2, CartTypeId = (int)CartType.SpecialDate },
            new CartDbResult { Id = 3, CartTypeId = (int)CartType.Vip }
        };

        cartMapperMock.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => cartDbResults.FirstOrDefault(c => c.Id == id));

        var cartProducts = new List<(int cartId, ProductDbResult product)>
        {
            (1, new ProductDbResult { Id = 101, Name = "product test 1", Price = 1234.50m }),
            (2, new ProductDbResult { Id = 102, Name = "product test 2", Price = 2345.75m }),
            (3, new ProductDbResult { Id = 103, Name = "product test 3", Price = 3456.90m })
        };

        // Setup que busca productos por cartId
        cartMapperMock.Setup(m => m.GetProductsByCartIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int cartId) =>
            {
                var products = cartProducts.Where(cp => cp.cartId == cartId)
                                         .Select(cp => cp.product)
                                         .ToList();
                return products;
            });


        return cartMapperMock;
    }
}

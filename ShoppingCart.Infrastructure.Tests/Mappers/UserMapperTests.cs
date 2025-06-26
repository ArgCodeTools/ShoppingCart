using ShoppingCart.Infrastructure.Mappers;
using ShoppingCart.Infrastructure.Tests.Environment;

namespace ShoppingCart.Infrastructure.Tests.Mappers;

public class UserMapperTests : IClassFixture<TestDatabaseEnvironment>
{
    private UserMapper _userMapper;

    public UserMapperTests(TestDatabaseEnvironment environment) {

        _userMapper = new UserMapper(environment.SqlExecutor);
    }


    [Fact]
    public async Task GetUserByIdAsync_WhenUserExists_ShouldReturnUser()
    {
        // Arrange
        const int existentUserId = 2;

        // Act
        var user = await _userMapper.GetUserByIdAsync(existentUserId);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(existentUserId, user.Id);
        Assert.Equal(1122334455, user.Dni);
        Assert.Equal("Roberto Gomez", user.Name);
        Assert.True(user.IsVip);
    }

    [Fact]
    public async Task GetUserByIdAsync_WhenUserExistsAndIsNotVip_ShouldReturnUser()
    {
        // Arrange
        const int existentUserId = 3;

        // Act
        var user = await _userMapper.GetUserByIdAsync(existentUserId);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(existentUserId, user.Id);
        Assert.Equal(66778899, user.Dni);
        Assert.Equal("Florinda Meza", user.Name);
        Assert.False(user.IsVip);
    }
}



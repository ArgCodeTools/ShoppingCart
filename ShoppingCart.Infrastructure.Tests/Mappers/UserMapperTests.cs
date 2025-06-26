using ShoppingCart.Infrastructure.Mappers;
using ShoppingCart.Infrastructure.Tests.Environment;

namespace ShoppingCart.Infrastructure.Tests.Mappers;

public class UserMapperTests : IClassFixture<TestDatabaseEnvironment>
{
    private UserMapper _userMapper;

    public UserMapperTests(TestDatabaseEnvironment environment) {

        _userMapper = new UserMapper(environment.SqlExecutor);
    }


    [Theory]
    [InlineData(2, 1122334455, "Roberto Gomez", true)]
    [InlineData(3, 66778899, "Florinda Meza", false)]
    public async Task GetUserByIdAsync_WhenUserExists_ShouldReturnUser(int id, long dni, string name, bool isVip)
    {
        // Act
        var user = await _userMapper.GetUserByIdAsync(id);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(id, user.Id);
        Assert.Equal(dni, user.Dni);
        Assert.Equal(name, user.Name);
        Assert.Equal(isVip, user.IsVip);
    }

    [Fact]
    public async Task GetUserByIdAsync_WhenUserDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        int nonExistentUserId = 999;

        // Act
        var user = await _userMapper.GetUserByIdAsync(nonExistentUserId);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task ExistsAsync_WithExistingDni_ShouldReturnTrue()
    {
        // Arrange
        long existingDni = 1122334455;

        // Act
        var exists = await _userMapper.ExistsAsync(existingDni);

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_WithNonExistingDni_ShouldReturnFalse()
    {
        // Arrange
        long nonExistingDni = 5544332211;

        // Act
        var exists = await _userMapper.ExistsAsync(nonExistingDni);

        // Assert
        Assert.False(exists);
    }

    [Theory]
    [InlineData(2, 1122334455, "Roberto Gomez", true)]
    [InlineData(3, 66778899, "Florinda Meza", false)]
    public async Task GetUserByDniAsync_WhenUserExists_ShouldReturnUser(int id, long dni, string name, bool isVip)
    {
        // Act
        var user = await _userMapper.GetUserByDniAsync(dni);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(id, user.Id);
        Assert.Equal(dni, user.Dni);
        Assert.Equal(name, user.Name);
        Assert.Equal(isVip, user.IsVip);
    }

    [Fact]
    public async Task GetUserByDniAsync_WhenUserDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        int nonExistentDni = 999;

        // Act
        var user = await _userMapper.GetUserByDniAsync(nonExistentDni);

        // Assert
        Assert.Null(user);
    }
}



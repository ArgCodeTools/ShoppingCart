using ShoppingCart.Application.Exceptions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Interfaces.Repositories;

namespace ShoppingCart.Application.Services;

public class EntityValidationService : IEntityValidationService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public EntityValidationService(
        ICartRepository cartRepository,
        IUserRepository userRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task ValidateCartExistsAsync(int cartId)
    {
        var exists = await _cartRepository.ExistsAsync(cartId);
        if (!exists)
        {
            throw new EntityNotFoundException("Carrito", cartId);
        }
    }

    public async Task ValidateUserExistsAsync(double userDni)
    {
        var exists = await _userRepository.ExistsAsync(userDni);
        if (!exists)
        {
            throw new EntityNotFoundException("Usuario", userDni);
        }
    }

    public async Task ValidateProductExistsAsync(int productId)
    {
        var exists = await _productRepository.ExistsAsync(productId);
        if (!exists)
        {
            throw new EntityNotFoundException("Producto", productId);
        }
    }
}
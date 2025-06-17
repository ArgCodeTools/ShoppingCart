using AutoMapper;
using ShoppingCart.Application.DTOs;
using ShoppingCart.Application.DTOs.Requests;
using ShoppingCart.Application.DTOs.Responses;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartFactory _cartFactory;
    private readonly ISpecialDateService _specialDateService;
    private readonly IEntityValidationService _entityValidationService;
    private readonly IMapper _mapper;

    public CartService(
        ICartRepository cartRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        ICartFactory cartFactory,
        ISpecialDateService specialDateService,
        IEntityValidationService entityValidationService,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _cartFactory = cartFactory;
        _specialDateService = specialDateService;
        _entityValidationService = entityValidationService;
        _mapper = mapper;
    }

    public async Task<CreateCartResponse> CreateCartAsync(CreateCartRequest request)
    {
        await _entityValidationService.ValidateUserExistsAsync(request.UserDni);

        var user = await _userRepository.GetByDniAsync(request.UserDni);
        
        var isSpecialDate = _specialDateService.IsSpecialDate();
        
        var cart = _cartFactory.CreateCart(user!, isSpecialDate);
        
        var createdCart = await _cartRepository.CreateAsync(cart);

        return new CreateCartResponse
        {
            CartId = createdCart.Id
        };
    }

    public async Task<bool> DeleteCartAsync(int cartId)
    {
        await _entityValidationService.ValidateCartExistsAsync(cartId);

        return await _cartRepository.DeleteAsync(cartId);
    }

    public async Task<CartDto> AddProductToCartAsync(AddProductToCartRequest request)
    {
        await _entityValidationService.ValidateCartExistsAsync(request.CartId);
        await _entityValidationService.ValidateProductExistsAsync(request.ProductId);

        var cart = await _cartRepository.GetByIdAsync(request.CartId);
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        cart!.AddItem(product!, request.Quantity);

        var updatedCart = await _cartRepository.UpdateAsync(cart);

        return _mapper.Map<CartDto>(updatedCart);
    }

    public async Task<CartDto> RemoveProductFromCartAsync(RemoveProductFromCartRequest request)
    {
        await _entityValidationService.ValidateCartExistsAsync(request.CartId);
        await _entityValidationService.ValidateProductExistsAsync(request.ProductId);

        var cart = await _cartRepository.GetByIdAsync(request.CartId);
        var product = await _productRepository.GetByIdAsync(request.ProductId);

        var removed = cart!.RemoveItem(product!);
        if (!removed)
        {
            throw new ArgumentException($"El producto con ID {request.ProductId} no está en el carrito");
        }

        var updatedCart = await _cartRepository.UpdateAsync(cart);

        return _mapper.Map<CartDto>(updatedCart);
    }

    public async Task<CartDto> GetCartStatusAsync(int cartId)
    {
        await _entityValidationService.ValidateCartExistsAsync(cartId);

        var cart = await _cartRepository.GetByIdAsync(cartId);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<List<ProductDto>> GetMostExpensiveProductsAsync(GetMostExpensiveProductsRequest request)
    {
        await _entityValidationService.ValidateUserExistsAsync(request.UserDni);

        var mostExpensiveProducts = await _productRepository.GetMostExpensiveByUserAsync(request.UserDni);

        return _mapper.Map<List<ProductDto>>(mostExpensiveProducts);
    }
}
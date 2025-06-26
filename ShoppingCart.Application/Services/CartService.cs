using AutoMapper;
using ShoppingCart.Application.DTOs;
using ShoppingCart.Application.DTOs.Inputs;
using ShoppingCart.Application.Dtos.Output;
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

    public async Task<CreateCartOutput> CreateCartAsync(CreateCartInput input)
    {
        await _entityValidationService.ValidateUserExistsAsync(input.UserDni);

        var user = await _userRepository.GetByDniAsync(input.UserDni);
        
        var isSpecialDate = _specialDateService.IsSpecialDate();
        
        var cart = _cartFactory.CreateCart(user!, isSpecialDate);
        
        var cartId = await _cartRepository.CreateAsync(cart);

        return new CreateCartOutput
        {
            CartId = cartId
        };
    }

    public async Task DeleteCartAsync(int cartId)
    {
        await _entityValidationService.ValidateCartExistsAsync(cartId);

        await _cartRepository.DeleteAsync(cartId);
    }

    public async Task<CartDto> AddProductToCartAsync(AddProductToCartInput input)
    {
        await _entityValidationService.ValidateCartExistsAsync(input.CartId);
        await _entityValidationService.ValidateProductExistsAsync(input.ProductId);

        var cart = await _cartRepository.GetByIdAsync(input.CartId);
        var product = await _productRepository.GetByIdAsync(input.ProductId);

        cart!.AddItem(product!);

        await _cartRepository.AddItemAsync(cart.Id, product!.Id);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> RemoveProductFromCartAsync(RemoveProductFromCartInput input)
    {
        await _entityValidationService.ValidateCartExistsAsync(input.CartId);
        await _entityValidationService.ValidateProductExistsAsync(input.ProductId);

        var cart = await _cartRepository.GetByIdAsync(input.CartId);
        var product = await _productRepository.GetByIdAsync(input.ProductId);

        var removed = cart!.RemoveItem(product!);
        if (!removed)
        {
            throw new ArgumentException($"El producto con ID {input.ProductId} no está en el carrito");
        }

        await _cartRepository.DeleteItemAsync(cart.Id, product!.Id);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> GetCartStatusAsync(int cartId)
    {
        await _entityValidationService.ValidateCartExistsAsync(cartId);

        var cart = await _cartRepository.GetByIdAsync(cartId);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<IEnumerable<ProductDto>> GetMostExpensiveProductsAsync(GetMostExpensiveProductsInput input)
    {
        await _entityValidationService.ValidateUserExistsAsync(input.UserDni);

        var mostExpensiveProducts = await _productRepository.GetMostExpensiveByUserAsync(input.UserDni);

        return _mapper.Map<IEnumerable<ProductDto>>(mostExpensiveProducts);
    }
}
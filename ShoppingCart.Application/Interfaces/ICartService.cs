using ShoppingCart.Application.DTOs;
using ShoppingCart.Application.DTOs.Inputs;
using ShoppingCart.Application.DTOs.Outputs;

namespace ShoppingCart.Application.Interfaces;

public interface ICartService
{
    Task<CreateCartOutput> CreateCartAsync(CreateCartInput input);
    Task DeleteCartAsync(int cartId);
    Task<CartDto> AddProductToCartAsync(AddProductToCartInput input);
    Task<CartDto> RemoveProductFromCartAsync(RemoveProductFromCartInput input);
    Task<CartDto> GetCartStatusAsync(int cartId);
    Task<List<ProductDto>> GetMostExpensiveProductsAsync(GetMostExpensiveProductsInput input);
}
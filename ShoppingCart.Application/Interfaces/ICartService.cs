using ShoppingCart.Application.DTOs;
using ShoppingCart.Application.DTOs.Inputs;
using ShoppingCart.Application.Dtos.Output;

namespace ShoppingCart.Application.Interfaces;

public interface ICartService
{
    Task<CreateCartOutput> CreateCartAsync(CreateCartInput input);
    Task DeleteCartAsync(int cartId);
    Task<CartDto> AddProductToCartAsync(AddProductToCartInput input);
    Task<CartDto> RemoveProductFromCartAsync(RemoveProductFromCartInput input);
    Task<CartDto> GetCartStatusAsync(int cartId);
    Task<IEnumerable<ProductDto>> GetMostExpensiveProductsAsync(GetMostExpensiveProductsInput input);
}
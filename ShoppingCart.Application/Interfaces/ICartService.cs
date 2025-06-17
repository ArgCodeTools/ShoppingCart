using ShoppingCart.Application.DTOs;
using ShoppingCart.Application.DTOs.Requests;
using ShoppingCart.Application.DTOs.Responses;

namespace ShoppingCart.Application.Interfaces;

public interface ICartService
{
    Task<CreateCartResponse> CreateCartAsync(CreateCartRequest request);
    Task<bool> DeleteCartAsync(int cartId);
    Task<CartDto> AddProductToCartAsync(AddProductToCartRequest request);
    Task<CartDto> RemoveProductFromCartAsync(RemoveProductFromCartRequest request);
    Task<CartDto> GetCartStatusAsync(int cartId);
    Task<List<ProductDto>> GetMostExpensiveProductsAsync(GetMostExpensiveProductsRequest request);
}
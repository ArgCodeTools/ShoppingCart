using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.DTOs.Inputs;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Contracts.Requests;

namespace ShoppingCart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    // POST /api/cart
    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request)
    {
        var input = _mapper.Map<CreateCartInput>(request);

        var output = await _cartService.CreateCartAsync(input);
        return CreatedAtAction(nameof(GetCartStatus), new { cartId = output.CartId }, output);
    }

    // DELETE /api/cart/{cartId}
    [HttpDelete("{cartId:int}")]
    public async Task<IActionResult> DeleteCart(int cartId)
    {
        await _cartService.DeleteCartAsync(cartId);
        return NoContent();
    }

    // POST /api/cart/{cartId}/items
    [HttpPost("{cartId:int}/items")]
    public async Task<IActionResult> AddProduct(int cartId, [FromBody] AddProductToCartRequest request)
    {
        var input = _mapper.Map<AddProductToCartInput>(request);
        input.CartId = cartId;

        var result = await _cartService.AddProductToCartAsync(input);

        return Ok(result);
    }

    // DELETE /api/cart/{cartId}/items/{productId}
    [HttpDelete("{cartId:int}/items/{productId:int}")]
    public async Task<IActionResult> RemoveProduct(int cartId, int productId)
    {
        var input = new RemoveProductFromCartInput
        {
            CartId = cartId,
            ProductId = productId
        };

        var result = await _cartService.RemoveProductFromCartAsync(input);
        return Ok(result);
    }

    // GET /api/cart/{cartId}
    [HttpGet("{cartId:int}")]
    public async Task<IActionResult> GetCartStatus(int cartId)
    {
        var result = await _cartService.GetCartStatusAsync(cartId);
        return Ok(result);
    }

    // GET /api/cart/user/{dni}/most-expensive-products
    [HttpGet("user/{dni}/most-expensive-products")]
    public async Task<IActionResult> GetMostExpensiveProducts(double dni)
    {
        var request = new GetMostExpensiveProductsInput { UserDni = dni };
        var result = await _cartService.GetMostExpensiveProductsAsync(request);
        return Ok(result);
    }
}

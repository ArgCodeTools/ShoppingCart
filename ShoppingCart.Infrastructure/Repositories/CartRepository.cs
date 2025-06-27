using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Entities.Carts;
using ShoppingCart.Domain.Enums;
using ShoppingCart.Infrastructure.Interfaces;

namespace ShoppingCart.Infrastructure.Repositories;

public class CartRepository(ICartMapper cartMapper, IUserMapper userMapper) : ICartRepository
{
    public async Task AddItemAsync(int cartId, int productId)
    {
        await cartMapper.AddItemAsync(cartId, productId);     
    }

    public async Task DeleteItemAsync(int cartId, int productId)
    {
        await cartMapper.DeleteItemAsync(cartId, productId);
    }

    public async Task<int> CreateAsync(CartBase cart)
    {
        return await cartMapper.CreateAsync(cart.User.Id, cart.Type);
    }

    public async Task DeleteAsync(int cartId)
    {
        await cartMapper.DeleteAsync(cartId);
    }

    public async Task<bool> ExistsAsync(int cartId)
    {
        return await cartMapper.ExistsAsync(cartId);
    }

    public async Task<CartBase?> GetByIdAsync(int cartId)
    {
        var cartDbResult = await cartMapper.GetByIdAsync(cartId);

        if (cartDbResult is null)
            return null;

        var user = await userMapper.GetUserByIdAsync(cartDbResult.UserId);

        CartBase cart = (CartType)cartDbResult.CartTypeId switch
        {
            CartType.Vip => new VipCart { User = user! },
            CartType.SpecialDate => new SpecialDateCart { User = user! },
            CartType.Common => new CommonCart { User = user! },
            _ => throw new NotImplementedException()
        };

        cart.Id = cartId;

        await FillProducts(cartId, cart);

        return cart;
    }

    private async Task FillProducts(int cartId, CartBase cart)
    {
        var products = await cartMapper.GetProductsByCartIdAsync(cartId);

        foreach (var productDbResult in products)
        {
            cart.AddItem(new Product()
            {
                Id = productDbResult.Id,
                Name = productDbResult.Name,
                Price = productDbResult.Price
            });
        }
    }
}
namespace ShoppingCart.Application.Interfaces;

public interface IEntityValidationService
{
    Task ValidateCartExistsAsync(int cartId);
    Task ValidateUserExistsAsync(long userDni);
    Task ValidateProductExistsAsync(int productId);
}
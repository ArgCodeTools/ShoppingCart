namespace ShoppingCart.Application.Interfaces;

public interface IEntityValidationService
{
    Task ValidateCartExistsAsync(int cartId);
    Task ValidateUserExistsAsync(double userDni);
    Task ValidateProductExistsAsync(int productId);
}
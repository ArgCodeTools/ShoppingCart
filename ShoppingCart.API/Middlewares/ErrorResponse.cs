namespace ShoppingCart.API.Middlewares;

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
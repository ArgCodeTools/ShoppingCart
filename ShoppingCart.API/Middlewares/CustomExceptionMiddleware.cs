namespace ShoppingCart.API.Middlewares;

using ShoppingCart.Application.Exceptions;
using System.Net;
using System.Text.Json;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse();

        switch (exception)
        {
            case ArgumentException:
            case FormatException:
            case InvalidOperationException:
            case EntityNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Solicitud inválida";
                response.Details = exception.Message;
                _logger.LogWarning("Bad Request: {Message}", exception.Message);
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.Message = "No autorizado";
                response.Details = "Acceso denegado";
                _logger.LogWarning("Unauthorized access attempt");
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Message = "Recurso no encontrado";
                response.Details = exception.Message;
                _logger.LogWarning("Resource not found: {Message}", exception.Message);
                break;

            default:
                // Para todas las demás excepciones, devolvemos 500
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "Error interno del servidor";
                response.Details = "Ocurrió un error inesperado";
                _logger.LogError(exception, "Unexpected error occurred");
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
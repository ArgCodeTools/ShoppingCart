using ShoppingCart.API.Mappings;
using ShoppingCart.Application;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.Interfaces.Repositories;
using ShoppingCart.Infrastructure.Models;
using ShoppingCart.Infrastructure.Repositories;
using ShoppingCart.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Habilita controllers con soporte para [ApiController]
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen(); // Genera documentación Swagger


builder.Services.Configure<SpecialDateConfiguration>(
    builder.Configuration.GetSection("SpecialDate"));

builder.Services.AddScoped<ISpecialDateService, SpecialDateService>();
builder.Services.AddApplication();

builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<ApiMappingProfile>(); });

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Habilita los endpoints

app.Run();

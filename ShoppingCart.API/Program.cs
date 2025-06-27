using ShoppingCart.API.Mappings;
using ShoppingCart.API.Middlewares;
using ShoppingCart.Application;
using ShoppingCart.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // Habilita controllers con soporte para [ApiController]
builder.Services.AddEndpointsApiExplorer(); // Necesario para Swagger
builder.Services.AddSwaggerGen(); // Genera documentacion Swagger

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<ApiMappingProfile>(); });

var app = builder.Build();

app.UseCustomExceptionMiddleware();

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

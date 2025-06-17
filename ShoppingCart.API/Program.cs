using ShoppingCart.Application.Interfaces;
using ShoppingCart.Infrastructure.Models;
using ShoppingCart.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SpecialDateConfiguration>(
builder.Configuration.GetSection("SpecialDate"));
builder.Services.AddScoped<ISpecialDateService, SpecialDateService>();

var app = builder.Build();

app.Run();

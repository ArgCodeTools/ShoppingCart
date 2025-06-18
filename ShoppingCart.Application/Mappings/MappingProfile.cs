using AutoMapper;
using ShoppingCart.Application.DTOs;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeos básicos
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();

        // Mapeo de CartItem a CartItemDto
        CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))            
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal));

        // Mapeo de ShoppingCartBase a CartDto
        CreateMap<CartBase, CartDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserDni, opt => opt.MapFrom(src => src.User.Dni))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))            
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CalculateTotal())); 
    }
}
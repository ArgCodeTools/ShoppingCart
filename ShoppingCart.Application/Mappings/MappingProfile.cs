using AutoMapper;
using ShoppingCart.Application.DTOs;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Entities.Carts;

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
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal));

        // Mapeo de ShoppingCartBase a CartDto
        CreateMap<CartBase, CartDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserDni, opt => opt.MapFrom(src => src.User.Dni))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.GetTotalItemCount()))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CalculateTotal())); 
    }
}
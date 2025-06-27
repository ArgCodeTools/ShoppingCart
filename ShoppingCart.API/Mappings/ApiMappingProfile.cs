using AutoMapper;
using ShoppingCart.API.Contracts.Requests;
using ShoppingCart.API.Contracts.Responses;
using ShoppingCart.Application.Dtos.Output;
using ShoppingCart.Application.DTOs;
using ShoppingCart.Application.DTOs.Inputs;

namespace ShoppingCart.API.Mappings;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // Requests → Inputs
        CreateMap<AddProductToCartRequest, AddProductToCartInput>();
        CreateMap<CreateCartRequest, CreateCartInput>();        

        // Outputs → Responses
        CreateMap<CreateCartOutput, CreateCartResponse>();

        // Dtos → Responses
        CreateMap<CartItemDto, CartItemResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

        CreateMap<CartDto, CartResponse>()
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.Items.Count()))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Items.Sum(x => x.Subtotal)));
    }
}

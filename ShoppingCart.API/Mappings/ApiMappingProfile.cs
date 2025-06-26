using AutoMapper;
using ShoppingCart.API.Contracts.Requests;
using ShoppingCart.API.Contracts.Responses;
using ShoppingCart.Application.DTOs.Inputs;
using ShoppingCart.Application.Dtos.Output;

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
    }
}

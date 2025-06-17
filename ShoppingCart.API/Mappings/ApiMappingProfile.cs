using AutoMapper;
using ShoppingCart.Application.DTOs.Inputs;
using ShoppingCart.Application.DTOs.Outputs;
using ShoppingCart.Contracts.Requests;
using ShoppingCart.Contracts.Responses;

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

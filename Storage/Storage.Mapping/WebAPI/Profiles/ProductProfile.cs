using AutoMapper;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.Common.Enums;
using Storage.Common.Models.DTOs.Product;

namespace Storage.Mapping.WebAPI.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductResponse, ProductDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.Stocks.Count > 0));

        CreateMap<CreateProductDTO, CreateProductRequest>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToEnum<ProductType>()));

        CreateMap<UpdateProductDTO, UpdateProductRequest>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToEnum<ProductType>()));
    }
}
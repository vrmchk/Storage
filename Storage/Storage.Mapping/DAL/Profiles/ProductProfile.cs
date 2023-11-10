using AutoMapper;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Stocks, opt => opt.MapAtRuntime())
            .ForMember(dest => dest.ProductReservations, opt => opt.MapAtRuntime());

        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
    }
}
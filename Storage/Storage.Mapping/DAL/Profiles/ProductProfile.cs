using AutoMapper;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.DAL.Entities;
using Storage.Mapping.DAL.MappingActions;

namespace Storage.Mapping.DAL.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Stocks, opt => opt.MapAtRuntime())
            .AfterMap<ProductToProductResponseAction>();

        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
    }
}
using AutoMapper;
using Storage.BLL.Responses.ProductReservation;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.Profiles;

public class ProductReservationProfile : Profile
{
    public ProductReservationProfile()
    {
        CreateMap<ProductReservation, ProductReservationResponse>();
    }
}
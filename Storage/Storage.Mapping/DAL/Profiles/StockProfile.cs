using AutoMapper;
using Storage.BLL.Responses.Stock;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockResponse>();
    }
}
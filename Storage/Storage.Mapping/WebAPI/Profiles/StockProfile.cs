using AutoMapper;
using Storage.BLL.Requests.Stock;
using Storage.BLL.Responses.Stock;
using Storage.Common.Models.DTOs.Stock;

namespace Storage.Mapping.WebAPI.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<StockResponse, StockDTO>();
        CreateMap<CreateStocksBatchDTO, CreateStocksBatchRequest>();
        CreateMap<UpdateStockDTO, UpdateStockRequest>();
    }
}
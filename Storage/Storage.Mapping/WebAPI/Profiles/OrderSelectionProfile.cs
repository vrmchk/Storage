using AutoMapper;
using Storage.BLL.Requests.Order.Models;
using Storage.BLL.Responses.OrderSelection;
using Storage.Common.Models.DTOs.OrderSelection;

namespace Storage.Mapping.WebAPI.Profiles;

public class OrderSelectionProfile : Profile
{
    public OrderSelectionProfile()
    {
        CreateMap<OrderSelectionResponse, OrderSelectionDTO>();
        CreateMap<CreateOrderSelectionDTO, CreateOrderSelectionModel>();
    }
}
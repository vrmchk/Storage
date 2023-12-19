using AutoMapper;
using Storage.BLL.Requests.Order.Models;
using Storage.BLL.Responses.OrderSelection;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.Profiles;

public class OrderSelectionProfile : Profile
{
    public OrderSelectionProfile()
    {
        CreateMap<OrderSelection, OrderSelectionResponse>();
        CreateMap<CreateOrderSelectionModel, OrderSelection>();
    }
}
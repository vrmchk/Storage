using AutoMapper;
using Storage.BLL.Requests.Order;
using Storage.BLL.Responses.Order;
using Storage.DAL.Entities;
using Storage.Mapping.DAL.MappingActions;

namespace Storage.Mapping.DAL.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.OrderSelections, opt => opt.MapAtRuntime())
            .AfterMap<OrderToOrderResponseAction>();

        CreateMap<CreateOrderRequest, Order>()
            .ForMember(dest => dest.OrderSelections, opt => opt.MapAtRuntime())
            .AfterMap<CreateOrderRequestToOrderAction>();

        CreateMap<UpdateOrderRequest, Order>();
    }
}
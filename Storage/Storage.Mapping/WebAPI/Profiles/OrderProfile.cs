using AutoMapper;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Order;
using Storage.BLL.Responses.Order;
using Storage.Common.Enums;
using Storage.Common.Models.DTOs.Order;
using Storage.Mapping.WebAPI.MappingActions;

namespace Storage.Mapping.WebAPI.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderResponse, OrderDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.OrderSelections, opt => opt.MapAtRuntime())
            .AfterMap<OrderResponseToOrderDTOAction>();

        CreateMap<CreateOrderDTO, CreateOrderRequest>()
            .ForMember(dest => dest.OrderSelections, opt => opt.MapAtRuntime())
            .AfterMap<CreateOrderDTOToCreateOrderRequestAction>();

        CreateMap<CreateOrderDTO, CheckOrderRequest>()
            .ForMember(dest => dest.OrderSelections, opt => opt.MapAtRuntime())
            .AfterMap<CreateOrderDTOToCheckOrderRequestAction>();

        CreateMap<CheckOrderResponse, CheckOrderResultDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<UpdateOrderDTO, UpdateOrderRequest>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnum<OrderStatus>()));

        CreateMap<SearchOrderDTO, SearchOrdersRequest>()
            .ForMember(dest => dest.Statuses, opt =>
                opt.MapFrom(src => (src.Statuses ?? new List<string>()).Select(x => x.ToEnum<OrderStatus>()).ToList()));
    }
}
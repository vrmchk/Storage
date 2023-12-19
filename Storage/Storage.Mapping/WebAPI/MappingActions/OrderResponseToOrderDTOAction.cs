using AutoMapper;
using Storage.BLL.Responses.Order;
using Storage.Common.Models.DTOs.Order;
using Storage.Common.Models.DTOs.OrderSelection;

namespace Storage.Mapping.WebAPI.MappingActions;

public class OrderResponseToOrderDTOAction : IMappingAction<OrderResponse, OrderDTO>
{
    public void Process(OrderResponse source, OrderDTO destination, ResolutionContext context)
    {
        destination.OrderSelections = context.Mapper.Map<List<OrderSelectionDTO>>(source.OrderSelections);
    }
}
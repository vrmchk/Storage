using AutoMapper;
using Storage.BLL.Responses.Order;
using Storage.BLL.Responses.OrderSelection;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.MappingActions;

public class OrderToOrderResponseAction : IMappingAction<Order, OrderResponse>
{
    public void Process(Order source, OrderResponse destination, ResolutionContext context)
    {
        destination.OrderSelections = context.Mapper.Map<List<OrderSelectionResponse>>(source.OrderSelections);
    }
}
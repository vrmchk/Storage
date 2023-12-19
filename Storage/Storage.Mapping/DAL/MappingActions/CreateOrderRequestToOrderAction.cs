using AutoMapper;
using Storage.BLL.Requests.Order;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.MappingActions;

public class CreateOrderRequestToOrderAction : IMappingAction<CreateOrderRequest, Order>
{
    public void Process(CreateOrderRequest source, Order destination, ResolutionContext context)
    {
        destination.OrderSelections = context.Mapper.Map<ICollection<OrderSelection>>(source.OrderSelections);
    }
}
using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Order;
using Storage.Common.Enums;

namespace Storage.BLL.Requests.Order;

public class UpdateOrderRequest : IRequestBase<OrderResponse>
{
    public Guid OrderId { get; set; }
    public OrderStatus Status { get; set; }
}
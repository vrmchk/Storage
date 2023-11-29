using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Order;
using Storage.Common.Enums;

namespace Storage.BLL.Requests.Order;

public class GetOrdersRequest : IRequestBase<List<OrderResponse>>
{
    public Guid UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public OrderStatus? Status { get; set; }
}
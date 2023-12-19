using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Order;

namespace Storage.BLL.Requests.Order;

public class GetOrderByIdRequest : IRequestBase<OrderResponse>
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
}
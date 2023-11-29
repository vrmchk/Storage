using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Order;
using Storage.Common.Enums;

namespace Storage.BLL.Requests.Order;

public class SearchOrdersRequest : IRequestBase<List<OrderResponse>>
{
    public List<Guid>? UserIds { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<OrderStatus>? Statuses { get; set; }
    public List<Guid>? ProductIds { get; set; }
    public List<Guid>? OrderIds { get; set; }
}
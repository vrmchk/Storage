using ErrorOr;
using Storage.BLL.Requests.Abstractions;

namespace Storage.BLL.Requests.Order;

public class DeleteOrderRequest : IRequestBase<Deleted>
{
    public Guid OrderId { get; set; }
}
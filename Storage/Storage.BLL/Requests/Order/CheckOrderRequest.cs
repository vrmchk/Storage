using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Requests.Order.Models;
using Storage.BLL.Responses.Order;

namespace Storage.BLL.Requests.Order;

public class CheckOrderRequest : IRequestBase<CheckOrderResponse>
{
    public Guid UserId { get; set; }
    public List<CreateOrderSelectionModel> OrderSelections { get; set; } = new();
}
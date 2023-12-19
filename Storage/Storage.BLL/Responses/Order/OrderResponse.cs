using Storage.BLL.Responses.OrderSelection;
using Storage.Common.Enums;

namespace Storage.BLL.Responses.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }

    public Guid UserId { get; set; }

    public List<OrderSelectionResponse> OrderSelections { get; set; } = new();
}
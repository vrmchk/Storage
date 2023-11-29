using Storage.Common.Models.DTOs.OrderSelection;

namespace Storage.Common.Models.DTOs.Order;

public class OrderDTO
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }

    public Guid UserId { get; set; }

    public List<OrderSelectionDTO> OrderSelections { get; set; } = new();
}
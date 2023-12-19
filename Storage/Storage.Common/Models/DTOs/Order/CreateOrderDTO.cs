using Storage.Common.Models.DTOs.OrderSelection;

namespace Storage.Common.Models.DTOs.Order;

public class CreateOrderDTO
{
    public List<CreateOrderSelectionDTO> OrderSelections { get; set; } = new();
}
namespace Storage.Common.Models.DTOs.OrderSelection;

public class CreateOrderSelectionDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
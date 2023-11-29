namespace Storage.Common.Models.DTOs.OrderSelection;

public class OrderSelectionDTO
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
}
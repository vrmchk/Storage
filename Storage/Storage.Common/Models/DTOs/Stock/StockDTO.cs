namespace Storage.Common.Models.DTOs.Stock;

public class StockDTO
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ProductId { get; set; }
    public Guid? OrderSelectionId { get; set; }
}
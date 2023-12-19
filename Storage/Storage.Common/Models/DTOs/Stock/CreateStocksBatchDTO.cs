namespace Storage.Common.Models.DTOs.Stock;

public class CreateStocksBatchDTO
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
}
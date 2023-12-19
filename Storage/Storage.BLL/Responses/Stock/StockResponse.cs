namespace Storage.BLL.Responses.Stock;

public class StockResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid ProductId { get; set; }
    public Guid? OrderSelectionId { get; set; }
}
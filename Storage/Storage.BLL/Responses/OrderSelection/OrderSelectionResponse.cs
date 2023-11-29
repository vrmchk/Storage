using Storage.BLL.Responses.Stock;

namespace Storage.BLL.Responses.OrderSelection;

public class OrderSelectionResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public List<StockResponse> Stocks { get; set; } = new();
}
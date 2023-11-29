using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Stock;

namespace Storage.BLL.Requests.Stock;

public class CreateStocksBatchRequest : IRequestBase<List<StockResponse>>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
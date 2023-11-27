using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Stock;

namespace Storage.BLL.Requests.Stock;

public class CreateStockRequest : IRequestBase<StockResponse>
{
    public Guid ProductId { get; set; }
}
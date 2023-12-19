using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Stock;

namespace Storage.BLL.Requests.Stock;

public class GetStocksRequest : IRequestBase<List<StockResponse>>
{
    public Guid ProductId { get; set; }
    public bool? IsAvailable { get; set; }
}
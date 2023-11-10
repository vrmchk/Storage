using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Stock;

namespace Storage.BLL.Requests.Stock;

public class GetStockByIdRequest : IRequestBase<StockResponse>
{
    public Guid Id { get; set; }
}
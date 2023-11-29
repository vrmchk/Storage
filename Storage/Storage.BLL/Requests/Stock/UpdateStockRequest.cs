using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Stock;

namespace Storage.BLL.Requests.Stock;

public class UpdateStockRequest : IRequestBase<StockResponse>
{
    public Guid Id { get; set; }
    public Guid OrderSelectionId { get; set; }
}
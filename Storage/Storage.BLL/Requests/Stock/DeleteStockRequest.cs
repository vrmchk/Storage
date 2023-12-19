using ErrorOr;
using Storage.BLL.Requests.Abstractions;

namespace Storage.BLL.Requests.Stock;

public class DeleteStockRequest : IRequestBase<Deleted>
{
    public Guid Id { get; set; }
}
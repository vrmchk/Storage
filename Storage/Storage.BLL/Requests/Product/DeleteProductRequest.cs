using ErrorOr;
using Storage.BLL.Requests.Abstractions;

namespace Storage.BLL.Requests.Product;

public class DeleteProductRequest : IRequestBase<Deleted>
{
    public Guid Id { get; set; }
}
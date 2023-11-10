using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Product;

namespace Storage.BLL.Requests.Product;

public class GetProductByIdRequest : IRequestBase<ProductResponse>
{
    public Guid Id { get; set; }
}
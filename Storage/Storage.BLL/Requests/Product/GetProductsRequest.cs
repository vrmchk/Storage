using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Product;
using Storage.Common.Enums;

namespace Storage.BLL.Requests.Product;

public class GetProductsRequest : IRequestBase<List<ProductResponse>>
{
    public ProductType ProductType { get; set; }
    public string? Name { get; set; } = string.Empty;
    public bool ShouldBeAvailable { get; set; }
}
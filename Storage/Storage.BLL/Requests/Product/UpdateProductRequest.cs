using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Product;
using Storage.Common.Enums;

namespace Storage.BLL.Requests.Product;

public class UpdateProductRequest : IRequestBase<ProductResponse>
{
    public Guid Id { get; set; }
    public ProductType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
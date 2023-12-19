using Storage.BLL.Responses.Stock;
using Storage.Common.Enums;

namespace Storage.BLL.Responses.Product;

public class ProductResponse
{
    public Guid Id { get; set; }
    public ProductType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public List<StockResponse> Stocks { get; set; } = new();

}
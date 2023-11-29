using AutoMapper;
using Storage.BLL.Responses.Product;
using Storage.BLL.Responses.Stock;
using Storage.DAL.Entities;

namespace Storage.Mapping.DAL.MappingActions;

public class ProductToProductResponseAction : IMappingAction<Product, ProductResponse>
{
    public void Process(Product source, ProductResponse destination, ResolutionContext context)
    {
        destination.Stocks = context.Mapper.Map<List<StockResponse>>(source.Stocks);
    }
}
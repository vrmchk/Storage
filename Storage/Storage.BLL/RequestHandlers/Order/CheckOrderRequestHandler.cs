using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Order;
using Storage.BLL.Requests.Order.Models;
using Storage.BLL.Responses.Order;
using Storage.Common.Enums;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Order;

public class CheckOrderRequestHandler : RequestHandlerBase<CheckOrderRequest, CheckOrderResponse>
{
    private readonly IRepository<E.Product> _productRepository;

    public CheckOrderRequestHandler(IValidator<CheckOrderRequest> validator,
        IRepository<E.Product> productRepository)
        : base(validator)
    {
        _productRepository = productRepository;
    }

    protected override async Task<ErrorOr<CheckOrderResponse>> HandleInternal(CheckOrderRequest request,
        CancellationToken cancellationToken)
    {
        var productIds = request.OrderSelections.Select(x => x.ProductId).ToList();

        var products = await _productRepository
            .Include(p => p.Stocks)
            .Where(p => productIds.Contains(p.Id))
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);

        if (products.Count != productIds.Count)
            return Error.NotFound("Some products do not exist");

        var selectionsWithProducts = request.OrderSelections
            .Join(products, s => s.ProductId, p => p.Id, (s, p) => new { Selection = s, Product = p })
            .ToList();

        var enoughStocks = selectionsWithProducts.All(x => x.Product.Stocks.Count >= x.Selection.Quantity);
        return new CheckOrderResponse
        {
            Status = enoughStocks ? CheckOrderResultStatus.CanBeProcessedNow : CheckOrderResultStatus.CanBeProcessedLater
        };
    }
}
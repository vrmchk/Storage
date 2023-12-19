using AutoMapper;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.Notifications;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Order;
using Storage.BLL.Responses.Order;
using Storage.Common.Enums;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Order;

public class CreateOrderRequestHandler : RequestHandlerBase<CreateOrderRequest, OrderResponse>
{
    private readonly IMapper _mapper;
    private readonly IRepository<E.Order> _repository;
    private readonly IRepository<E.Product> _productRepository;
    private readonly IMediator _mediator;

    public CreateOrderRequestHandler(IValidator<CreateOrderRequest> validator,
        IMapper mapper,
        IRepository<E.Order> repository,
        IRepository<E.Product> productRepository,
        IMediator mediator)
        : base(validator)
    {
        _mapper = mapper;
        _repository = repository;
        _productRepository = productRepository;
        _mediator = mediator;
    }

    protected override async Task<ErrorOr<OrderResponse>> HandleInternal(CreateOrderRequest request,
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

        var order = _mapper.Map<E.Order>(request);
        order.CreatedAt = DateTime.UtcNow;
        order.Status = OrderStatus.Created;
        order.Amount = selectionsWithProducts.Sum(x => x.Product.Price * x.Selection.Quantity);

        await _repository.InsertAsync(order, cancellationToken);

        await _mediator.Publish(new OrderCreatedNotification { OrderId = order.Id }, cancellationToken);
        
        return _mapper.Map<OrderResponse>(order);
    }
}
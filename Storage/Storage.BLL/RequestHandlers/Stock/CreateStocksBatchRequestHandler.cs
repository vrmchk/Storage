using AutoMapper;
using ErrorOr;
using FluentValidation;
using MediatR;
using Storage.BLL.Notifications;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Stock;
using Storage.BLL.Responses.Stock;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Stock;

public class CreateStocksBatchRequestHandler : RequestHandlerBase<CreateStocksBatchRequest, List<StockResponse>>
{
    private readonly IRepository<E.Stock> _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateStocksBatchRequestHandler(IValidator<CreateStocksBatchRequest> validator,
        IRepository<E.Stock> repository,
        IMapper mapper,
        IMediator mediator) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    protected override async Task<ErrorOr<List<StockResponse>>> HandleInternal(CreateStocksBatchRequest request,
        CancellationToken cancellationToken)
    {
        var stocks = Enumerable.Range(0, request.Quantity).Select(_ => new E.Stock
        {
            ProductId = request.ProductId
        }).ToList();

        await _repository.InsertManyAsync(stocks);

        var notification = new StocksAddedNotification { ProductId = request.ProductId };
        await _mediator.Publish(notification, cancellationToken);

        return _mapper.Map<List<StockResponse>>(stocks);
    }
}
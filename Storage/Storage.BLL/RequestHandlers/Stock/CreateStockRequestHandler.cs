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

public class CreateStockRequestHandler : RequestHandlerBase<CreateStockRequest, StockResponse>
{
    private readonly IRepository<E.Stock> _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateStockRequestHandler(IValidator<CreateStockRequest> validator,
        IRepository<E.Stock> repository,
        IMapper mapper,
        IMediator mediator) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    protected override async Task<ErrorOr<StockResponse>> HandleInternal(CreateStockRequest request,
        CancellationToken cancellationToken)
    {
        var stock = _mapper.Map<E.Stock>(request);

        await _repository.InsertAsync(stock);

        await _mediator.Publish(new StockAddedNotification { ProductId = request.ProductId }, cancellationToken);

        return _mapper.Map<StockResponse>(stock);
    }
}
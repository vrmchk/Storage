using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Order;
using Storage.BLL.Responses.Order;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Order;

public class GetOrdersRequestHandler : RequestHandlerBase<GetOrdersRequest, List<OrderResponse>>
{
    private readonly IRepository<E.Order> _repository;
    private readonly IMapper _mapper;

    public GetOrdersRequestHandler(IValidator<GetOrdersRequest> validator,
        IRepository<E.Order> repository,
        IMapper mapper)
        : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<OrderResponse>>> HandleInternal(GetOrdersRequest request,
        CancellationToken cancellationToken)
    {
        var queryable = _repository
            .Include(o => o.OrderSelections)
            .ThenInclude(o => o.Product)
            .Include(o => o.OrderSelections)
            .ThenInclude(o => o.Stocks)
            .Where(o => o.UserId == request.UserId);

        if (request.Status != null)
            queryable = queryable.Where(o => o.Status == request.Status);

        if (request.From != null)
            queryable = queryable.Where(o => o.CreatedAt >= request.From);

        if (request.To != null)
            queryable = queryable.Where(o => o.CreatedAt <= request.To);

        var orders = await queryable.ToListAsync(cancellationToken);
        return _mapper.Map<List<OrderResponse>>(orders);
    }
}
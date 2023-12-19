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

public class SearchOrdersRequestHandler : RequestHandlerBase<SearchOrdersRequest, List<OrderResponse>>
{
    private readonly IRepository<E.Order> _repository;
    private readonly IMapper _mapper;

    public SearchOrdersRequestHandler(IValidator<SearchOrdersRequest> validator,
        IRepository<E.Order> repository,
        IMapper mapper)
        : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<OrderResponse>>> HandleInternal(SearchOrdersRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<E.Order> queryable = _repository
            .Include(o => o.OrderSelections)
            .ThenInclude(o => o.Product)
            .Include(o => o.OrderSelections)
            .ThenInclude(o => o.Stocks);

        if (request.From != null)
            queryable = queryable.Where(o => o.CreatedAt >= request.From);

        if (request.To != null)
            queryable = queryable.Where(o => o.CreatedAt <= request.To);

        if (request.OrderIds?.Count > 0)
            queryable = queryable.Where(o => request.OrderIds.Contains(o.Id));

        if (request.Statuses?.Count > 0)
            queryable = queryable.Where(o => request.Statuses.Contains(o.Status));

        if (request.UserIds?.Count > 0)
            queryable = queryable.Where(o => request.UserIds.Contains(o.UserId));

        if (request.ProductIds?.Count > 0)
            queryable = queryable.Where(o => o.OrderSelections.Any(os => request.ProductIds.Contains(os.ProductId)));

        var orders = await queryable.ToListAsync(cancellationToken);
        return _mapper.Map<List<OrderResponse>>(orders);
    }
}
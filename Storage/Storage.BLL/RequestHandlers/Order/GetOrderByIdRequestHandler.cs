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

public class GetOrderByIdRequestHandler : RequestHandlerBase<GetOrderByIdRequest, OrderResponse>
{
    private readonly IRepository<E.Order> _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdRequestHandler(IValidator<GetOrderByIdRequest> validator,
        IRepository<E.Order> repository,
        IMapper mapper)
        : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<OrderResponse>> HandleInternal(GetOrderByIdRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _repository
            .Include(o => o.OrderSelections)
            .ThenInclude(o => o.Product)
            .Include(o => o.OrderSelections)
            .ThenInclude(o => o.Stocks)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == request.UserId,
            cancellationToken);

        if (order == null)
            return Error.NotFound("Order with this id does not exist");

        return _mapper.Map<OrderResponse>(order);
    }
}
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

public class UpdateOrderRequestHandler : RequestHandlerBase<UpdateOrderRequest, OrderResponse>
{
    private readonly IRepository<E.Order> _repository;
    private readonly IMapper _mapper;

    public UpdateOrderRequestHandler(IValidator<UpdateOrderRequest> validator,
        IRepository<E.Order> repository,
        IMapper mapper)
        : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<OrderResponse>> HandleInternal(UpdateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        if (order == null)
            return Error.NotFound("Order with this id does not exist");

        _mapper.Map(request, order);

        await _repository.UpdateAsync(order);

        return _mapper.Map<OrderResponse>(order);
    }
}
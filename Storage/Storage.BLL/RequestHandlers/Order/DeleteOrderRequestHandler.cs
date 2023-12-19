using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Order;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Order;

public class DeleteOrderRequestHandler : RequestHandlerBase<DeleteOrderRequest, Deleted>
{
    private readonly IRepository<E.Order> _repository;

    public DeleteOrderRequestHandler(IValidator<DeleteOrderRequest> validator,
        IRepository<E.Order> repository)
        : base(validator)
    {
        _repository = repository;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteOrderRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        if (order == null)
            return Error.NotFound("Order with this id does not exist");

        await _repository.DeleteAsync(order, cancellationToken);

        return Result.Deleted;
    }
}
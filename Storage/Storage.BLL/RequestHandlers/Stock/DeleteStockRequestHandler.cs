using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Stock;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Stock;

public class DeleteStockRequestHandler : RequestHandlerBase<DeleteStockRequest, Deleted>
{
    private readonly IRepository<E.Stock> _repository;    

    public DeleteStockRequestHandler(IValidator<DeleteStockRequest> validator, IRepository<E.Stock> repository)
        : base(validator)
    {
        _repository = repository;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteStockRequest request, CancellationToken cancellationToken)
    {
        var stock = await _repository.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (stock == null)
            return Error.NotFound("Stock with this id does not exist");
        
        await _repository.DeleteAsync(stock);
        return Result.Deleted;
    }
}
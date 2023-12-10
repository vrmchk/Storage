using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Product;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Product;

public class DeleteProductRequestHandler : RequestHandlerBase<DeleteProductRequest, Deleted>
{
    private readonly IRepository<E.Product> _repository;

    public DeleteProductRequestHandler(IValidator<DeleteProductRequest> validator, IRepository<E.Product> repository)
        : base(validator)
    {
        _repository = repository;
    }

    protected override async Task<ErrorOr<Deleted>> HandleInternal(DeleteProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (product == null)
            return Error.NotFound("Product with this id does not exist");

        await _repository.DeleteAsync(product, cancellationToken);
        return Result.Deleted;
    }
}
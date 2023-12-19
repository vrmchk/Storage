using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Product;

public class UpdateProductRequestHandler : RequestHandlerBase<UpdateProductRequest, ProductResponse>
{
    private readonly IRepository<E.Product> _repository;
    private readonly IMapper _mapper;

    public UpdateProductRequestHandler(IValidator<UpdateProductRequest> validator,
        IRepository<E.Product> repository,
        IMapper mapper) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<ProductResponse>> HandleInternal(UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.SingleOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        if (product == null)
            return Error.NotFound("Product with this id does not exist");

        _mapper.Map(request, product);
        await _repository.UpdateAsync(product, cancellationToken);

        return _mapper.Map<ProductResponse>(product);
    }
}
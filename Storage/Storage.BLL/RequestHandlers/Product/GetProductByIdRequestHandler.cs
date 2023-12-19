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

public class GetProductByIdRequestHandler : RequestHandlerBase<GetProductByIdRequest, ProductResponse>
{
    private readonly IRepository<E.Product> _repository;
    private readonly IMapper _mapper;

    public GetProductByIdRequestHandler(IValidator<GetProductByIdRequest> validator,
        IRepository<E.Product> repository,
        IMapper mapper) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<ProductResponse>> HandleInternal(GetProductByIdRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _repository
            .Include(p => p.Stocks)
            .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            return Error.NotFound("Product with this id does not exist");

        return _mapper.Map<ProductResponse>(product);
    }
}
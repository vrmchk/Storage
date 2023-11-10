using AutoMapper;
using ErrorOr;
using FluentValidation;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Product;

public class CreateProductRequestHandler : RequestHandlerBase<CreateProductRequest, ProductResponse>
{
    private readonly IRepository<E.Product> _repository;
    private readonly IMapper _mapper;

    public CreateProductRequestHandler(IValidator<CreateProductRequest> validator,
        IRepository<E.Product> repository,
        IMapper mapper) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<ProductResponse>> HandleInternal(CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = _mapper.Map<E.Product>(request);
        await _repository.InsertAsync(product);
        return _mapper.Map<ProductResponse>(product);
    }
}
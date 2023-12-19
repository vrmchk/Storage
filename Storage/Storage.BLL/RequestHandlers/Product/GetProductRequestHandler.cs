using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.Common.Enums;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Product;

public class GetProductRequestHandler : RequestHandlerBase<GetProductsRequest, List<ProductResponse>>
{
    private readonly IRepository<E.Product> _repository;
    private readonly IMapper _mapper;

    public GetProductRequestHandler(IValidator<GetProductsRequest> validator,
        IRepository<E.Product> repository,
        IMapper mapper) :
        base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<ProductResponse>>> HandleInternal(GetProductsRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<E.Product> queryable = _repository
            .Include(p => p.Stocks.Where(s => s.OrderSelectionId == null));

        if (request.ProductType != ProductType.None)
            queryable = queryable.Where(p => p.Type == request.ProductType);

        if (!string.IsNullOrEmpty(request.Name))
            queryable = queryable.Where(p => p.Name.Contains(request.Name));

        if (request.IsAvailable != null)
        {
            queryable = request.IsAvailable.Value
                ? queryable.Where(p => p.Stocks.Count > 0)
                : queryable.Where(p => p.Stocks.Count == 0);
        }

        return _mapper.Map<List<ProductResponse>>(await queryable.ToListAsync(cancellationToken));
    }
}
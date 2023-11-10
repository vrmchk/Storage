using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Stock;
using Storage.BLL.Responses.Stock;
using Storage.DAL.Repositories.Interfaces;
using E = Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Stock;

public class GetStocksRequestHandler : RequestHandlerBase<GetStocksRequest, List<StockResponse>>
{
    private readonly IRepository<E.Stock> _repository;
    private readonly IMapper _mapper;

    public GetStocksRequestHandler(IValidator<GetStocksRequest> validator,
        IRepository<E.Stock> repository,
        IMapper mapper) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<StockResponse>>> HandleInternal(GetStocksRequest request,
        CancellationToken cancellationToken)
    {
        var stocks = await _repository.Where(s => s.ProductId == request.ProductId).ToListAsync(cancellationToken);
        return _mapper.Map<List<StockResponse>>(stocks);
    }
}
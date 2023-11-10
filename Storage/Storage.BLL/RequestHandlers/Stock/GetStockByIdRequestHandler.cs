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

public class GetStockByIdRequestHandler : RequestHandlerBase<GetStockByIdRequest, StockResponse>
{
    private readonly IRepository<E.Stock> _repository;
    private readonly IMapper _mapper;

    public GetStockByIdRequestHandler(IValidator<GetStockByIdRequest> validator,
        IRepository<E.Stock> repository,
        IMapper mapper) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<StockResponse>> HandleInternal(GetStockByIdRequest request,
        CancellationToken cancellationToken)
    {
        var stocks = await _repository.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (stocks == null)
            return Error.NotFound("Stock with this id does not exist");

        return _mapper.Map<StockResponse>(stocks);
    }
}
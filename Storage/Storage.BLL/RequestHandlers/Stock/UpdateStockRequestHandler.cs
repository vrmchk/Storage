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

public class UpdateStockRequestHandler : RequestHandlerBase<UpdateStockRequest, StockResponse>
{
    private readonly IRepository<E.Stock> _repository;
    private readonly IMapper _mapper;

    public UpdateStockRequestHandler(IValidator<UpdateStockRequest> validator,
        IRepository<E.Stock> repository,
        IMapper mapper) : base(validator)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<StockResponse>> HandleInternal(UpdateStockRequest request,
        CancellationToken cancellationToken)
    {
        var stock = await _repository.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (stock == null)
            return Error.NotFound("Stock with this id does not exist");

        stock = _mapper.Map(request, stock);
        await _repository.UpdateAsync(stock);

        return _mapper.Map<StockResponse>(stock);
    }
}
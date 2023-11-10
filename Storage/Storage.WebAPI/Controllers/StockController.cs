using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Requests.Stock;
using Storage.BLL.Responses.Stock;
using Storage.Common.Models.DTOs;
using Storage.Common.Models.DTOs.Stock;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api")]
public class StockController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StockController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("products/{productId}/stocks")]
    public async Task<IActionResult> GetStocksByProductId(Guid productId)
    {
        var result = await _mediator.Send(new GetStocksRequest { ProductId = productId });
        return _mapper.ToActionResult<List<StockResponse>, List<StockDTO>>(result);
    }

    [HttpGet("/stocks/{stockId}")]
    public async Task<IActionResult> GetStockById(Guid stockId)
    {
        var result = await _mediator.Send(new GetStockByIdRequest { Id = stockId });
        return _mapper.ToActionResult<StockResponse, StockDTO>(result);
    }

    [HttpPost("/stocks")]
    public async Task<IActionResult> AddStock([FromBody] CreateStockDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateStockRequest>(dto));
        return result.Match<IActionResult>(
            stock => CreatedAtAction(nameof(GetStockById), new { stockId = stock.Id }, stock),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("/stocks/{stockId}")]
    public async Task<IActionResult> UpdateStock(Guid stockId, [FromBody] UpdateStockDTO dto)
    {
        if (stockId != dto.Id)
            return BadRequest(new ErrorDTO { Errors = new[] { "Ids have to be the same in route and body" } });

        var result = await _mediator.Send(_mapper.Map<UpdateStockRequest>(dto));
        return _mapper.ToActionResult<StockResponse, StockDTO>(result);
    }

    [HttpDelete("/stocks/{stockId}")]
    public async Task<IActionResult> DeleteStock(Guid stockId)
    {
        var result = await _mediator.Send(new DeleteStockRequest { Id = stockId });
        return result.ToActionResult();
    }
}
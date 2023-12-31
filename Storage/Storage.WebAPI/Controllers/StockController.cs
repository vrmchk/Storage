﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Requests.Stock;
using Storage.BLL.Responses.Stock;
using Storage.Common.Constants;
using Storage.Common.Models.DTOs.Stock;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api/stocks")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationRoles.Manager)]
public class StockController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StockController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockById(Guid id)
    {
        var result = await _mediator.Send(new GetStockByIdRequest { Id = id });
        return _mapper.ToActionResult<StockResponse, StockDTO>(result);
    }

    [HttpPost("batch")]
    public async Task<IActionResult> AddStock([FromBody] CreateStocksBatchDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateStocksBatchRequest>(dto));
        return _mapper.ToActionResult<List<StockResponse>, List<StockDTO>>(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock(Guid id)
    {
        var result = await _mediator.Send(new DeleteStockRequest { Id = id });
        return result.ToActionResult();
    }
}
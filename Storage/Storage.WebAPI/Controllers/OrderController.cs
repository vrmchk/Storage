using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Order;
using Storage.BLL.Responses.Order;
using Storage.Common.Constants;
using Storage.Common.Enums;
using Storage.Common.Models.DTOs.Order;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    
    [HttpGet]
    [Authorize(Roles = ApplicationRoles.User)]
    public async Task<IActionResult> GetOrders(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        [FromQuery] string? status = null)
    {
        var result = await _mediator.Send(new GetOrdersRequest
        {
            UserId = HttpContext.GetUserId(),
            From = from,
            To = to,
            Status = status?.ToEnum<OrderStatus>()
        });
        return _mapper.ToActionResult<List<OrderResponse>, List<OrderDTO>>(result);
    }

    [HttpPost("search")]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> SearchOrders([FromBody] SearchOrderDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<SearchOrdersRequest>(dto));
        return _mapper.ToActionResult<List<OrderResponse>, List<OrderDTO>>(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{ApplicationRoles.User},{ApplicationRoles.Manager}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var result = await _mediator.Send(new GetOrderByIdRequest
        {
            OrderId = id,
            UserId = HttpContext.GetUserId()
        });
        return _mapper.ToActionResult<OrderResponse, OrderDTO>(result);
    }

    [HttpPost("check")]
    [Authorize(Roles = ApplicationRoles.User)]
    public async Task<IActionResult> CheckOrder([FromBody] CreateOrderDTO dto)
    {
        var request = _mapper.Map<CheckOrderRequest>(dto);
        request.UserId = HttpContext.GetUserId();

        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<CheckOrderResponse, CheckOrderResultDTO>(result);
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.User)]
    public async Task<IActionResult> AddOrder([FromBody] CreateOrderDTO dto)
    {
        var request = _mapper.Map<CreateOrderRequest>(dto);
        request.UserId = HttpContext.GetUserId();

        var result = await _mediator.Send(request);
        return result.Match<IActionResult>(
            order => CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("{id}")]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDTO dto)
    {
        var request = _mapper.Map<UpdateOrderRequest>(dto);
        request.OrderId = id;
        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<OrderResponse, OrderDTO>(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var result = await _mediator.Send(new DeleteOrderRequest { OrderId = id });
        return result.ToActionResult();
    }
}
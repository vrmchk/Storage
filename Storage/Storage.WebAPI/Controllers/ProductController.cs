using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Product;
using Storage.BLL.Requests.Stock;
using Storage.BLL.Responses.Product;
using Storage.BLL.Responses.Stock;
using Storage.Common.Constants;
using Storage.Common.Enums;
using Storage.Common.Models.DTOs.Product;
using Storage.Common.Models.DTOs.Stock;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api/products")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = $"{ApplicationRoles.User},{ApplicationRoles.Manager}")]
    public async Task<IActionResult> GetProducts(
        [FromQuery] string? productType = null,
        [FromQuery] string? name = null,
        [FromQuery] bool? isAvailable = null)
    {
        var result = await _mediator.Send(new GetProductsRequest
        {
            ProductType = productType?.ToEnum<ProductType>() ?? ProductType.None,
            Name = name,
            IsAvailable = isAvailable
        });

        return _mapper.ToActionResult<List<ProductResponse>, List<ProductDTO>>(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{ApplicationRoles.User},{ApplicationRoles.Manager}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdRequest { Id = id });
        return _mapper.ToActionResult<ProductResponse, ProductDTO>(result);
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateProductRequest>(dto));
        return result.Match<IActionResult>(
            product => CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("{id}")]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDTO dto)
    {
        var request = _mapper.Map<UpdateProductRequest>(dto);
        request.ProductId = id;
        var result = await _mediator.Send(request);
        return _mapper.ToActionResult<ProductResponse, ProductDTO>(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductRequest { Id = id });
        return result.ToActionResult();
    }

    [HttpGet("{id}/stocks")]
    [Authorize(Roles = ApplicationRoles.Manager)]
    public async Task<IActionResult> GetStocksByProductId(Guid productId, [FromQuery] bool? isAvailable = null)
    {
        var result = await _mediator.Send(new GetStocksRequest
        {
            ProductId = productId,
            IsAvailable = isAvailable
        });
        return _mapper.ToActionResult<List<StockResponse>, List<StockDTO>>(result);
    }
}
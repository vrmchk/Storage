using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Extensions;
using Storage.BLL.Requests.Product;
using Storage.BLL.Responses.Product;
using Storage.Common.Enums;
using Storage.Common.Models.DTOs;
using Storage.Common.Models.DTOs.Product;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api/products")]
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
    public async Task<IActionResult> GetProducts(
        [FromQuery] string? productType = null,
        [FromQuery] string? name = null,
        [FromQuery] bool shouldBeAvailable = false)
    {
        var result = await _mediator.Send(new GetProductsRequest
        {
            ProductType = productType?.ToEnum<ProductType>() ?? ProductType.None,
            Name = name,
            ShouldBeAvailable = shouldBeAvailable
        });

        return _mapper.ToActionResult<List<ProductResponse>, List<ProductDTO>>(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdRequest { Id = id });
        return _mapper.ToActionResult<ProductResponse, ProductDTO>(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<CreateProductRequest>(dto));
        return result.Match<IActionResult>(
            product => CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product),
            error => BadRequest(error.ToErrorDTO())
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDTO dto)
    {
        if (id != dto.Id)
            return BadRequest(new ErrorDTO { Errors = new[] { "Ids have to be the same in route and body" } });

        var result = await _mediator.Send(_mapper.Map<UpdateProductRequest>(dto));
        return _mapper.ToActionResult<ProductResponse, ProductDTO>(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductRequest { Id = id });
        return result.ToActionResult();
    }
}
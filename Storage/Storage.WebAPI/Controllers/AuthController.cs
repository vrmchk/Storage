using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Requests.Auth;
using Storage.BLL.Responses.Auth;
using Storage.Common.Models.DTOs.Auth;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<SignUpRequest>(dto));
        return _mapper.ToActionResult<AuthSuccessResponse, AuthSuccessDTO>(result);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<SignInRequest>(dto));
        return _mapper.ToActionResult<AuthSuccessResponse, AuthSuccessDTO>(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<RefreshTokenRequest>(dto));
        return _mapper.ToActionResult<AuthSuccessResponse, AuthSuccessDTO>(result);
    }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.BLL.Requests.Users;
using Storage.BLL.Responses.Users;
using Storage.Common.Constants;
using Storage.Common.Models.DTOs.User;
using Storage.WebAPI.Extensions;

namespace Storage.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationRoles.Manager)]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper; 
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchUsers([FromBody] SearchUsersDTO dto)
    {
        var result = await _mediator.Send(_mapper.Map<SearchUsersRequest>(dto));
        return _mapper.ToActionResult<List<UserResponse>, List<UserDTO>>(result);
    }
    
    [HttpPost("make-manager/{userId}")]
    public async Task<IActionResult> MakeManager(Guid userId)
    {
        var result = await _mediator.Send(new MakeManagerRequest { UserId = userId });
        return result.ToActionResult();
    }
}
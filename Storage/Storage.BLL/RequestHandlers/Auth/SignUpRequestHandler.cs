using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Storage.BLL.Requests.Auth;
using Storage.BLL.Responses.Auth;
using Storage.Common.Models.Configs;
using Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Auth;

public class SignUpRequestHandler : AuthRequestHandlerBase<SignUpRequest>
{
    private readonly IMapper _mapper;

    public SignUpRequestHandler(IValidator<SignUpRequest> validator,
        JwtConfig jwtConfig,
        UserManager<User> userManager,
        IMapper mapper)
        : base(validator, jwtConfig, userManager)
    {
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<AuthSuccessResponse>> HandleInternal(SignUpRequest request,
        CancellationToken cancellationToken)
    {
        var user = await UserManager.FindByEmailAsync(request.Email);
        if (user is not null)
            return Error.Failure("User with this email already exists");

        user = _mapper.Map<User>(request);
        var createdUser = await UserManager.CreateAsync(user, request.Password);
        if (!createdUser.Succeeded)
            return Error.Failure("Unable to create a user. Please try again later or use another email address");

        return await GenerateAuthResultAsync(user);
    }
}
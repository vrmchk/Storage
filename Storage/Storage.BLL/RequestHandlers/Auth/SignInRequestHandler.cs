using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Storage.BLL.Requests.Auth;
using Storage.BLL.Responses.Auth;
using Storage.Common.Models.Configs;
using Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Auth;

public class SignInRequestHandler : AuthRequestHandlerBase<SignInRequest>
{
    public SignInRequestHandler(IValidator<SignInRequest> validator, JwtConfig jwtConfig, UserManager<User> userManager)
        : base(validator, jwtConfig, userManager) { }

    protected override async Task<ErrorOr<AuthSuccessResponse>> HandleInternal(SignInRequest request,
        CancellationToken cancellationToken)
    {
        var user = await UserManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Error.NotFound("User with this email does not exist");
        
        var validPassword = await UserManager.CheckPasswordAsync(user, request.Password);
        if (!validPassword)
            return Error.Failure("Email or password is incorrect");

        return await GenerateAuthResultAsync(user);
    }
}
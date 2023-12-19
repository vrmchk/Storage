using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Storage.BLL.Requests.Auth;
using Storage.BLL.Responses.Auth;
using Storage.Common.Models.Configs;
using Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Auth;

public class RefreshTokenHandler : AuthRequestHandlerBase<RefreshTokenRequest>
{
    private readonly TokenValidationParameters _tokenValidationParameters;

    public RefreshTokenHandler(IValidator<RefreshTokenRequest> validator,
        JwtConfig jwtConfig,
        UserManager<User> userManager,
        TokenValidationParameters tokenValidationParameters)
        : base(validator, jwtConfig, userManager)
    {
        _tokenValidationParameters = tokenValidationParameters;
    }

    protected override async Task<ErrorOr<AuthSuccessResponse>> HandleInternal(RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var validatedToken = GetPrincipalFromToken(request.AccessToken);
        if (validatedToken == null)
            return Error.Validation("Access token is invalid");

        var expiryDateUnix =
            long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);

        if (expiryDateTimeUtc > DateTime.UtcNow)
            return Error.Failure("Access token is not expired yet");

        var user = await UserManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
        if (user is null)
            return Error.NotFound("User with this id does not exist");

        if (DateTimeOffset.UtcNow > user.RefreshTokenExpiresAt)
            return Error.Failure("Refresh token is expired");

        if (user.RefreshToken != request.RefreshToken)
            return Error.Failure("Refresh token is invalid");

        return await GenerateAuthResultAsync(user);
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = _tokenValidationParameters.Clone();
        validationParameters.ValidateLifetime = false;
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            return HasValidSecurityAlgorithm(validatedToken) ? principal : null;
        }
        catch
        {
            return null;
        }
    }

    private bool HasValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}
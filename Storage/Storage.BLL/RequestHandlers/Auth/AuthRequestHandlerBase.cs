using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Storage.BLL.Extensions;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Auth;
using Storage.BLL.Utility;
using Storage.Common.Models.Configs;
using Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Auth;

public abstract class AuthRequestHandlerBase<TRequest> : RequestHandlerBase<TRequest, AuthSuccessResponse>
    where TRequest : IRequestBase<AuthSuccessResponse>
{
    private readonly JwtConfig _jwtConfig;
    protected readonly UserManager<User> UserManager;

    protected AuthRequestHandlerBase(IValidator<TRequest> validator, JwtConfig jwtConfig, UserManager<User> userManager)
        : base(validator)
    {
        _jwtConfig = jwtConfig;
        UserManager = userManager;
    }

    protected async Task<ErrorOr<AuthSuccessResponse>> GenerateAuthResultAsync(User user)
    {
        var result = await GenerateRefreshTokenAsync(user);
        return await result.Match<Task<ErrorOr<AuthSuccessResponse>>>(async refreshToken => new AuthSuccessResponse
            {
                AccessToken = await GenerateJwtToken(user),
                RefreshToken = refreshToken
            },
            error => Task.FromResult<ErrorOr<AuthSuccessResponse>>(error));
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var result = await UserManager.GetRoleAsync(user);
        result.Switch(role => claims.Add(new Claim(ClaimTypes.Role, role)), _ => { });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtConfig.AccessTokenLifetime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }

    private async Task<ErrorOr<string>> GenerateRefreshTokenAsync(User user)
    {
        user.RefreshToken = TokenGenerator.GenerateToken();
        user.RefreshTokenExpiresAt = DateTimeOffset.UtcNow.Add(_jwtConfig.RefreshTokenLifetime);
        var userUpdated = await UserManager.UpdateAsync(user);
        if (!userUpdated.Succeeded)
            return Error.Failure("Unable to refresh token");

        return user.RefreshToken;
    }
}
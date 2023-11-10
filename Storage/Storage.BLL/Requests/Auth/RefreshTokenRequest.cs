using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Auth;

namespace Storage.BLL.Requests.Auth;

public class RefreshTokenRequest : IRequestBase<AuthSuccessResponse>
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
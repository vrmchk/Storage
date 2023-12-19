using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Auth;

namespace Storage.BLL.Requests.Auth;

public class SignInRequest : IRequestBase<AuthSuccessResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
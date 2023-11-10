using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Auth;

namespace Storage.BLL.Requests.Auth;

public class SignUpRequest : IRequestBase<AuthSuccessResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
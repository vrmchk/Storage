namespace Storage.BLL.Responses.Auth;

public class AuthSuccessResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
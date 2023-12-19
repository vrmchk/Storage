using Storage.BLL.Requests.Abstractions;
using Storage.BLL.Responses.Users;

namespace Storage.BLL.Requests.Users;

public class SearchUsersRequest : IRequestBase<List<UserResponse>>
{
    public List<Guid>? UserIds { get; set; }
    public List<string>? Emails { get; set; }
    public List<string>? UserNames { get; set; }
}
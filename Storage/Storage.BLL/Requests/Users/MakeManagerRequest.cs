using ErrorOr;
using Storage.BLL.Requests.Abstractions;

namespace Storage.BLL.Requests.Users;

public class MakeManagerRequest : IRequestBase<Success>
{
    public Guid UserId { get; set; }
}
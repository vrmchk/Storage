using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Storage.BLL.Extensions;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Users;
using Storage.BLL.Responses.Users;
using Storage.Common.Constants;
using Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Users;

public class MakeManagerRequestHandler : RequestHandlerBase<MakeManagerRequest, Success>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public MakeManagerRequestHandler(IValidator<MakeManagerRequest> validator,
        UserManager<User> userManager,
        IMapper mapper)
        : base(validator)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<Success>> HandleInternal(MakeManagerRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Error.NotFound("User with this id does not exist");

        var result = await _userManager.SetRoleAsync(user, ApplicationRoles.Manager);
        if (!result.Succeeded)
            return Error.Failure("Failed to make user manager");

        return Result.Success;
    }
}
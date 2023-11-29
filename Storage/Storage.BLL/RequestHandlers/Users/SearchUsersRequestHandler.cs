using AutoMapper;
using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.RequestHandlers.Abstractions;
using Storage.BLL.Requests.Users;
using Storage.BLL.Responses.Order;
using Storage.BLL.Responses.Users;
using Storage.DAL.Entities;

namespace Storage.BLL.RequestHandlers.Users;

public class SearchUsersRequestHandler : RequestHandlerBase<SearchUsersRequest, List<UserResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;    

    public SearchUsersRequestHandler(IValidator<SearchUsersRequest> validator, 
        UserManager<User> userManager,
        IMapper mapper) 
        : base(validator)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    protected override async Task<ErrorOr<List<UserResponse>>> HandleInternal(SearchUsersRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<User> queryable = _userManager.Users;

        if (request.UserIds?.Count > 0)
            queryable = queryable.Where(u => request.UserIds.Contains(u.Id));

        if (request.Emails?.Count > 0)
            queryable = queryable.Where(u => request.Emails.Contains(u.Email!));

        if (request.UserNames?.Count > 0)
            queryable = queryable.Where(u => request.UserNames.Contains(u.DisplayName));

        return _mapper.Map<List<UserResponse>>(await queryable.ToListAsync(cancellationToken));
    }
}
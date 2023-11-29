using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Storage.BLL.Extensions;

public static class UserManagerExtensions
{
    public static async Task<IdentityResult> SetRoleAsync<TUser>(this UserManager<TUser> userManager, TUser user,
        string role)
        where TUser : class
    {
        var roles = await userManager.GetRolesAsync(user);
        if (roles.Contains(role))
            return IdentityResult.Success;

        var removeResult = await userManager.RemoveFromRolesAsync(user, roles);
        if (!removeResult.Succeeded)
            return removeResult;
 
        return await userManager.AddToRoleAsync(user, role);
    }

    public static async Task<ErrorOr<string>> GetRoleAsync<TUser>(this UserManager<TUser> userManager, TUser user)
        where TUser : class
    {
        var roles = await userManager.GetRolesAsync(user);
        if (roles.Count == 1)
            return roles.First();

        return Error.Failure("User either has no roles or has more than one role");
    }
}
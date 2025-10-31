using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null || !httpContext.User.Identity!.IsAuthenticated)
        {
            return null;
        }
        var userId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)!.Value; // get user id from claims
        var email = httpContext.User.FindFirst(u => u.Type == ClaimTypes.Email)!.Value;
        var roles = httpContext.User.FindAll(u => u.Type == ClaimTypes.Role).Select(r => r.Value)!;
        return new CurrentUser(userId, email, roles);

    }
}

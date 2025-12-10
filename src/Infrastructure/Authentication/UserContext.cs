using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    /// <summary>
    /// Gets the email address of the current authenticated user (mapped from the 'email' claim).
    /// </summary>
    public string UserEmail =>
        _httpContextAccessor.HttpContext?.User.GetUserEmail() ??
        throw new ApplicationException("User email claim is unavailable.");
}

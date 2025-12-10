using System.Security.Claims;

namespace Infrastructure.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            throw new InvalidOperationException("User is not authenticated or user ID claim is missing.");
        }
        return userId;

        //return Guid.TryParse(userId, out Guid parsedUserId) ?
        //    parsedUserId :
        //    throw new ApplicationException("User id is unavailable");
    }

    /// <summary>
    /// Retrieves the Email from the ClaimsPrincipal.
    /// </summary>
    public static string? GetUserEmail(this ClaimsPrincipal principal)
    {
        // Retrieve the email from the standard JWT 'email' claim
        return principal.FindFirst(ClaimTypes.Email)?.Value;
    }
}

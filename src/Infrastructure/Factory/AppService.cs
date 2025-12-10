using Application.Abstractions.Factory;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Factory;

internal sealed class AppService(IHttpContextAccessor httpContextAccessor) : IAppService
{
    public string GetBaseLink()
    {
        HttpRequest? request = httpContextAccessor.HttpContext?.Request;
        if (request == null)
        {
            return null;
        }

        string scheme = request.Scheme;
        string host = request.Host.ToUriComponent();
        string pathBase = request.PathBase.ToUriComponent();

        string baseUrl = $"{scheme}://{host}{pathBase.TrimEnd('/')}/";
        return baseUrl;
    }

    public string ConvertEmailToName(string email) =>
       string.IsNullOrEmpty(email) ? "" : email.Split('@')[0].Replace(".", " ");
}

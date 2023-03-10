using Microsoft.AspNetCore.Http;
using Stix.Core.Interfaces;

namespace Stix.Web.Common
{
    public class HttpContextRequestingUser : IRequestingUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextRequestingUser(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        }
    }
}
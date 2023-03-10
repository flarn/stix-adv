using Microsoft.AspNetCore.Http;
using Stix.Core.Interfaces;

namespace Stix.Web.Common
{
    public class HttpContextAuthAccessor : IAuthAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextAuthAccessor(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
        public Core.Models.Auth? GetAuthContext()
        {
            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == false)
                return null;

            return new Core.Models.Auth();
        }
    }
}
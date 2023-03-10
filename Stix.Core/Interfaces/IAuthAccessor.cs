using Stix.Core.Models;

namespace Stix.Core.Interfaces
{
    public interface IAuthAccessor
    {
        Auth? GetAuthContext();
    }
}

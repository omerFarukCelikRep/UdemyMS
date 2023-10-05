using Microsoft.AspNetCore.Http;
using UdemyMS.Common.Utilities;

namespace UdemyMS.Common.Web.Services;
public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor?.HttpContext?.User?.FindFirst(Constants.Identity.Claim.Sub)?.Value;
}

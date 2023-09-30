using Microsoft.AspNetCore.Identity;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;

namespace UdemyMS.ExternalServices.IdentityServer.Controllers;
public class UsersController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    public UsersController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;
using UdemyMS.ExternalServices.IdentityServer.Models.Requests;

namespace UdemyMS.ExternalServices.IdentityServer.Controllers;
public class UsersController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    public UsersController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await _userManager.CreateAsync(request, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description)
                                      .ToList();
            return BadRequest(Result.Error(errors, (int)HttpStatusCode.BadRequest));
        }

        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using UdemyMS.Common.Core.Utilities.Results;
using UdemyMS.Common.Web.Controllers;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;
using UdemyMS.ExternalServices.IdentityServer.Models.Requests;
using UdemyMS.ExternalServices.IdentityServer.Models.Responses;
using static Duende.IdentityServer.IdentityServerConstants;

namespace UdemyMS.ExternalServices.IdentityServer.Controllers;

[Authorize(LocalApi.PolicyName)]
public class UsersController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    public UsersController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("signup")]
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

    [HttpGet]
    public async Task<IActionResult> GetUserAsync(CancellationToken cancellationToken = default)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim is null)
            return BadRequest();

        var user = await _userManager.FindByIdAsync(userIdClaim.Value);
        if (user is null)
            return BadRequest();

        return GetResult(Result<GetUserResponse>.Success(user, (int)HttpStatusCode.OK));
    }
}

using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;

namespace UdemyMS.ExternalServices.IdentityServer.Validators;

public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<AppUser> _userManager;
    public IdentityResourceOwnerPasswordValidator(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByEmailAsync(context.UserName);
        if (user is null)
        {
            AssignErrors(context);
            return;
        }

        var passwordCheck = await _userManager.CheckPasswordAsync(user, context.Password);
        if (!passwordCheck)
        {
            AssignErrors(context);
            return;
        }

        context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
    }

    private static void AssignErrors(ResourceOwnerPasswordValidationContext context)
    {
        var errors = new Dictionary<string, object>()
        {
            {"errors",new List<string> {"Email vaya şifreniz yanlıştır"}}
        };

        context.Result.CustomResponse = errors;
    }
}
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;

namespace UdemyMS.ExternalServices.IdentityServer.Models.Requests;

public class SignUpRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public static implicit operator AppUser(SignUpRequest request) => new()
    {
        UserName = request.UserName,
        Email = request.Email,
        City = request.City,
    };
}

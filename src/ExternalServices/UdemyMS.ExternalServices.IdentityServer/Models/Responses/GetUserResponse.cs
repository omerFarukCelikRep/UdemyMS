using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;

namespace UdemyMS.ExternalServices.IdentityServer.Models.Responses;

public class GetUserResponse
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? City { get; set; }

    public static implicit operator GetUserResponse(AppUser user)
    {
        return new GetUserResponse
        {
            Id = user.Id,
            Username = user.UserName!,
            Email = user.Email!,
            City = user.City,
        };
    }
}

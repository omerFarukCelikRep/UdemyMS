using Microsoft.AspNetCore.Identity;

namespace UdemyMS.ExternalServices.IdentityServer.Data.DbSets;

public class AppUser : IdentityUser
{
    public string City { get; set; }
}

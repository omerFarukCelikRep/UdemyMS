using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;

namespace UdemyMS.ExternalServices.IdentityServer.Data.Context;

public class IdentityServerDbContext : IdentityDbContext<AppUser>
{
    public const string Connection = "Default";

    public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
    {

    }
}

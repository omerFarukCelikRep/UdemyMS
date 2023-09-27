using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UdemyMS.ExternalServices.IdentityServer.Data;

public class IdentityServerDbContext : IdentityDbContext
{
    public const string Connection = "Default";

    public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
    {

    }
}

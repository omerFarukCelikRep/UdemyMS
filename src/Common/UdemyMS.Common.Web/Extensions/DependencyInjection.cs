using Microsoft.Extensions.DependencyInjection;
using UdemyMS.Common.Web.Services;

namespace UdemyMS.Common.Web.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddCommonWebServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddHttpContextAccessor();

        return services;
    }
}

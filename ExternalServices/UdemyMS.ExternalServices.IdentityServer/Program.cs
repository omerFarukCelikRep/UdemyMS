using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UdemyMS.ExternalServices.IdentityServer.Data.Context;
using UdemyMS.ExternalServices.IdentityServer.Data.DbSets;
using UdemyMS.ExternalServices.IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

await MigrateDbAsync();

app.Run();


async Task MigrateDbAsync()
{
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<IdentityServerDbContext>();

    dbContext?.Database.Migrate();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

    if (userManager != null && !await userManager.Users.AnyAsync())
        await userManager.CreateAsync(new AppUser { UserName = "ofcelik", Email = "email@email.com", City = "Ýstanbul" }, "newPassword+0");
}
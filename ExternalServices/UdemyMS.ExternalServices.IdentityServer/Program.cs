using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UdemyMS.ExternalServices.IdentityServer.Data;
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
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (userManager != null && !await userManager.Users.AnyAsync())
        await userManager.CreateAsync(new IdentityUser { UserName = "ofcelik", Email = "email@email.com" }, "newPassword+0");
}
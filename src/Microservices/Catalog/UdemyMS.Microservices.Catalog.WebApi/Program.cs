using UdemyMS.Microservices.Catalog.Infrastructure.Extensions;
using UdemyMS.Microservices.Catalog.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddInfrastructureServices()
                    .AddWebApiServices(builder.Configuration);  
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

}

app.Run();
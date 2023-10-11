using UdemyMS.Microservices.Discount.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5)
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

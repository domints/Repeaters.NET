using Microsoft.EntityFrameworkCore;
using Repeaters.NET.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RepeatersContext>(options =>
    options.UseMySQL($"server={builder.Configuration["Database:Host"]};uid={builder.Configuration["Database:Username"]};pwd={builder.Configuration["Database:Password"]};database={builder.Configuration["Database:Name"]}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezingu", "Bracingu", "Chillyu", "Coolu", "Mildu", "Warmu", "Balmyu", "Hotu", "Swelteringu", "Scorchingu"
};

app.MapGet("/api/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/api/checkdb", (RepeatersContext dbContext) =>
{
    return dbContext.Database.ExecuteSqlRawAsync("SELECT version()");
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

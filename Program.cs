using Microsoft.EntityFrameworkCore;
using Entidades.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

// responsible for database connection
builder.Services.AddDbContext<ApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetValue<string>("SampleDbConnection");
    options.UseNpgsql(connectionString);
});

// repositories and services
builder.Services.AddScoped<Entidades.Repositories.Interfaces.ITeamRepository, Entidades.Repositories.TeamRepository>();
builder.Services.AddScoped<Entidades.Repositories.Interfaces.IDriverRepository, Entidades.Repositories.DriverRepository>();
builder.Services.AddScoped<Entidades.Services.Interfaces.ITeamService, Entidades.Services.TeamService>();
builder.Services.AddScoped<Entidades.Services.Interfaces.IDriverService, Entidades.Services.DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// apply pending EF Core migrations at startup (will attempt to connect to DB)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    db.Database.Migrate();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapControllers();

// keep sample endpoint
app.MapGet("/weatherforecast", () =>
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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

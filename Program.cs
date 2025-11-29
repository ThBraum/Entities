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
builder.Services.AddScoped<Entidades.Repositories.Interfaces.IDriverMediaRepository, Entidades.Repositories.DriverMediaRepository>();
builder.Services.AddScoped<Entidades.Services.Interfaces.IDriverMediaService, Entidades.Services.DriverMediaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// apply pending EF Core migrations at startup (will attempt to connect to DB)
// Use retry/backoff so the app doesn't crash if Postgres is still starting.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    const int maxAttempts = 12;
    const int delayMs = 2000;
    var attempted = 0;
    var migrated = false;

    while (attempted < maxAttempts && !migrated)
    {
        attempted++;
        try
        {
            app.Logger.LogInformation("Attempt {Attempt} to apply migrations", attempted);
            db.Database.Migrate();
            migrated = true;
            app.Logger.LogInformation("Migrations applied (or database already up-to-date)");
            break;
        }
        catch (Exception ex)
        {
            app.Logger.LogWarning(ex, "Attempt {Attempt} failed to apply migrations. Retrying in {Delay}ms...", attempted, delayMs);
            if (attempted >= maxAttempts)
            {
                app.Logger.LogError(ex, "Exceeded migration attempts. Will attempt EnsureCreated() as a fallback");
                try
                {
                    db.Database.EnsureCreated();
                    migrated = true;
                    app.Logger.LogInformation("EnsureCreated() succeeded as fallback");
                }
                catch (Exception inner)
                {
                    app.Logger.LogError(inner, "EnsureCreated also failed after retries");
                    throw;
                }
            }
            else
            {
                System.Threading.Thread.Sleep(delayMs);
            }
        }
    }

    // After migrations/ensure created, verify domain tables exist; attempt EnsureCreated again if needed
    try
    {
        if (!db.Database.CanConnect())
        {
            app.Logger.LogWarning("Database is not reachable after migration attempts");
        }
        else
        {
            var hasTeams = db.Database.ExecuteSqlRaw("SELECT 1 FROM pg_catalog.pg_tables WHERE schemaname='public' AND tablename='Teams'") == 1;
            if (!hasTeams)
            {
                app.Logger.LogWarning("'Teams' table not found after migration; calling EnsureCreated()");
                db.Database.EnsureCreated();
            }
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Verification of tables failed; attempting EnsureCreated()");
        try
        {
            db.Database.EnsureCreated();
        }
        catch (Exception inner)
        {
            app.Logger.LogError(inner, "Final EnsureCreated failed");
            throw;
        }
    }
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

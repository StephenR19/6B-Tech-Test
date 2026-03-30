using Microsoft.EntityFrameworkCore;
using SixBee.Api.Data;
using SixBee.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDataProtection();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
builder.Services.AddScoped<SixBee.Api.Services.AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retries = 10;
    while (true)
    {
        try
        {
            db.Database.EnsureCreated();
            DbSeeder.Seed(db);
            break;
        }
        catch (Exception) when (retries > 0)
        {
            retries--;
            Thread.Sleep(3000);
        }
    }
}

app.UseCors("AllowFrontend");
app.UseAuth();
app.MapControllers();

app.Run();

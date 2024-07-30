using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Endpoints;
using upword.Api.Entities;
using upword.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Get connection string from configuration
var connString = builder.Configuration.GetConnectionString("upword");

// Register SQLite database context
builder.Services.AddSqlite<upwordContext>(connString);

// Register HttpClient for making API calls
builder.Services.AddHttpClient();

// Register WordService with IServiceScopeFactory
builder.Services.AddScoped<WordService>(provider => new WordService(
    "Data/vocabulary.json",
    provider.GetRequiredService<IHttpClientFactory>(),
    provider.GetRequiredService<IServiceScopeFactory>()
));

// Add Identity services with custom options
builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Configure Identity options
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<upwordContext>()
    .AddDefaultTokenProviders();

// Add authorization services
builder.Services.AddAuthorization();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:5174").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowFrontend");

// Use authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map your endpoints for words
app.MapWordsEndpoints();

// Map your authentication endpoints
app.MapAuthEndpoints();

// Ensure database is up-to-date
await app.MigrateDbAsync();

// Add a unique word on startup
using (var scope = app.Services.CreateScope())
{
    var wordService = scope.ServiceProvider.GetRequiredService<WordService>();
    await wordService.AddUniqueWordAsync(); // This will run on startup
}

app.Run();

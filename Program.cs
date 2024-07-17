using upword.Api.Data;
using upword.Api.Endpoints;
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

var app = builder.Build();

// Map your endpoints for words
app.MapWordsEndpoints();

// Ensure database is up-to-date
await app.MigrateDbAsync();

// Add a unique word on startup
using (var scope = app.Services.CreateScope())
{
    var wordService = scope.ServiceProvider.GetRequiredService<WordService>();
    await wordService.AddUniqueWordAsync(); // This will run on startup
}

app.Run();

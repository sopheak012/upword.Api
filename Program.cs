using upword.Api.Data;
using upword.Api.Endpoints;
using upword.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("upword");

builder.Services.AddSqlite<upwordContext>(connString);

// builder.Services.AddScoped<WordService>(provider => new WordService(
//     "data/vocabulary.json",
//     provider.GetRequiredService<upwordContext>()
// ));

var app = builder.Build();

// Map your endpoints for words
app.MapWordsEndpoints();

await app.MigrateDbAsync();

app.Run();

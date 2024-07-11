using upword.Api.Data;
using upword.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("upword");
builder.Services.AddSqlite<upwordContext>(connString);

var app = builder.Build();

app.MapWordsEndpoints();

app.MigrateDb();

app.Run();

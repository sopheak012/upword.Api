using upword.Api.Dtos;
using upword.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapWordsEndpoints();

app.Run();

using Catalog.API.Services.Abstract;
using Catalog.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoService, MongoService>();

var app = builder.Build(); 

app.MapGet("/", (IMongoService mongoService) =>
{
	return Results.Ok("test");
});

await app.RunAsync();

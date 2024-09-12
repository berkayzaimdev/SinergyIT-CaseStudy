using Catalog.API.Models;
using Catalog.API.Services.Abstract;
using Catalog.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoService, MongoService>();

var app = builder.Build();  

app.MapGet("/", (IMongoService mongoService) =>
{
	var collection = mongoService.GetCollection<Brand>();
	return Results.Ok("test");
});

await app.RunAsync();

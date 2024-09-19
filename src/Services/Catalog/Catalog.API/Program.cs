using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.RegisterApplicationServices();

app.Run();

// TODO: will try to fix launch browser problem

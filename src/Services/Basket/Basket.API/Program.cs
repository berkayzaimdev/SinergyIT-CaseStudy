using Basket.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.RegisterApplicationServices();

app.Run();

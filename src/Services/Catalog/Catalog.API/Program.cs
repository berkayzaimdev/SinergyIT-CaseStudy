using Catalog.API.Extensions;
using Catalog.API.Models;
using Catalog.API.Services.Abstract;
using Catalog.API.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

var app = builder.Build();

app.RegisterApplicationServices();

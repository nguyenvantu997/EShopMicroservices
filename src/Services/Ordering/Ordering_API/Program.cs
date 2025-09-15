using Ordering.Application;
using Ordering.Domain;
using Ordering.Infrastructure;
using Ordering_API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddDomainServices()
    .AddApplicationServices();

var app = builder.Build();

app.UseApiService();

app.Run();

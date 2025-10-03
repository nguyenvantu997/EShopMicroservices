using Ordering.Application;
using Ordering.Domain;
using Ordering.Infrastructure;
using Ordering_API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddDomainServices()
    .AddApiServices();

var app = builder.Build();

app.UseApiService();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();

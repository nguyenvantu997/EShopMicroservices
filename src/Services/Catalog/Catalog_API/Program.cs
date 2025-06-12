using Carter;

var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
       .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
       .ToArray();

    config.RegisterServicesFromAssemblies(assemblies);
});

var app = builder.Build();
app.MapCarter();
// config the HTTP request pipline
app.Run();

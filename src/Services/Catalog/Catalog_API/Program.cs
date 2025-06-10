var builder = WebApplication.CreateBuilder(args);

// add services to the container

var app = builder.Build();

// config the HTTP request pipline
app.Run();

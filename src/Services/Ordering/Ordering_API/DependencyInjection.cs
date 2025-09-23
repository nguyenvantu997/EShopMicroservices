using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Extensions;

namespace Ordering_API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            return services;
        }

        public static WebApplication UseApiService(this WebApplication app)
        {
            return app;
        }

        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await DatabaseExtensions.SeedAsync(context);
        }
    }
}

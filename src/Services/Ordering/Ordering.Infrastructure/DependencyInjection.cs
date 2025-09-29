using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Database");
            services.AddScoped<AuditEntityInterceptor>();
            services.AddScoped<DispatchDomainEventsInterceptor>();
            services.AddDbContext<ApplicationDbContext>((sp, opts) =>
            {
                var interceptor = sp.GetRequiredService<AuditEntityInterceptor>();
                var domainEventsInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptor>();
                opts.AddInterceptors(interceptor, domainEventsInterceptor)
                    .UseSqlServer(connection);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
           services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy", builder =>
                   builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());
           });

        // add IIS configure options deploy to IIS
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<SmartDriveContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("SmartDriveDB"));
            });
    }
}

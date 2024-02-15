using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence.SO;
using Service.Abstraction.SO;
using Service.Base;

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
        // register repository manager
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositorySOManager, RepositorySOManager>();

        // register service Manager
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceSOManager, ServiceSOManager>();

        // register service request manager
        public static void ConfigureServiceRequestManager(this IServiceCollection services) =>
            services.AddScoped<IServiceRequestSOManager, ServiceRequestSOManager>();
    }
}

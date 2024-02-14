using Domain.Repositories.Master;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence.Repositories.Master;
using Service.Abstraction.Master;
using Service.Master;

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

        public static void ConfigureRepositoryManagerMaster(this IServiceCollection services) => services.AddScoped<IRepositoryManagerMaster, RepositoryManagerMaster>();
        public static void ConfigureServiceManagerMaster(this IServiceCollection services) => services.AddScoped<IServiceManagerMaster, ServiceManagerMaster>();
    }
}
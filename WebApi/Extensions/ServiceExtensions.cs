using Domain.Repositories.CR;
using Domain.Repositories.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Persistence.Repositories;
using Persistence.Repositories.CR;
using Service.Abstraction.CR;
using Service.CR;
using Persistence.Repositories.UserModule;
using Service.Abstraction.User;
using Service.Base.UserModule;

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

        public static void ConfigureCustomerRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryCustomerManager, RepositoryCustomerManager>();

        public static void ConfigureCustomerServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceCustomerManager, ServiceCustomerManager>();

        public static void ConfigureRepositoryUser(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManagerUser, RepositoryManagerUser>();
        public static void ConfigureServiceUser(this IServiceCollection services) =>
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();

    }
}

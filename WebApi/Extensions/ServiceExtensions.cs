using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Persistence.Repositories;
using Service.Abstraction.Payment;
using Service.Base;
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

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryPaymentManager, RepositoryPaymentManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
          services.AddScoped<IServicePaymentManager, ServicePaymentManager>();

        public static void ConfigureRepositoryUser(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManagerUser, RepositoryManagerUser>();
        public static void ConfigureServiceUser(this IServiceCollection services) =>
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();

    }
}

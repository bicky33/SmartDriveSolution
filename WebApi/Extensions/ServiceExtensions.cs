using Contract.DTO.Partners;
using Domain.Entities.Partners;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.Repositories.UserModule;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Persistence.Repositories;
using Persistence.Repositories.Partners;
using Persistence.Repositories.UserModule;
using Service.Abstraction.Partners;
using Service.Abstraction.User;
using Service.Base.UserModule;
using Service.Partners;
using System.Reflection;

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

        public static void ConfigureRepositoryUser(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManagerUser, RepositoryManagerUser>();
        public static void ConfigureServiceUser(this IServiceCollection services) =>
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();
        public static void ConfigureRepositoryPartner(this IServiceCollection services) =>
            services.AddScoped<IRepositoryPartnerManager, RepositoryPartnerManager>();
        public static void ConfigureServicePartner(this IServiceCollection services) =>
            services.AddScoped<IServicePartnerManager, ServicePartnerManager>();

        public static void ConfigureMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
        }


    }
}

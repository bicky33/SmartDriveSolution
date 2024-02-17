using Contract.DTO.Partners;
using Domain.Entities.Partners;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.Repositories.UserModule;
using Mapster;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Persistence.SO;
using Persistence.Repositories;
using Persistence.Repositories.Partners;
using Persistence.Repositories.UserModule;
using Service.Abstraction.Partners;
using Persistence.Repositories.Master;
using Persistence.Repositories.SO;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Service.Abstraction.User;
using Service.Base.UserModule;
using Service.Partners;
using System.Reflection;
using Service.Master;
using Service.SO;
using Domain.Repositories.Master;

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

        public static void ConfigureRepository(this IServiceCollection services)  {
            services.AddScoped<IRepositoryManagerMaster, RepositoryManagerMaster>();
            services.AddScoped<IRepositoryManagerUser, RepositoryManagerUser>();
            services.AddScoped<IRepositorySOManager, RepositorySOManager>();
            services.AddScoped<IRepositoryPartnerManager, RepositoryPartnerManager>();
        }
        public static void ConfigureService(this IServiceCollection services) {
            services.AddScoped<IServiceManagerMaster, ServiceManagerMaster>();
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();
            services.AddScoped<IServiceSOManager, ServiceSOManager>();
            services.AddScoped<IServiceRequestSOManager, ServiceRequestSOManager>();
            services.AddScoped<IServicePartnerManager, ServicePartnerManager>();
        }
        public static void ConfigureMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
        }
    }
}
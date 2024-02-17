using Domain.Repositories.Master;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Persistence.SO;
using Persistence.Repositories;
using Persistence.Repositories.Master;
using Persistence.Repositories.SO;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Service.Abstraction.Payment;
using Service.Base;
using Persistence.Repositories.UserModule;
using Service.Abstraction.User;
using Service.Base.UserModule;
using Service.Master;
using Service.SO;

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

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManagerMaster, RepositoryManagerMaster>();
            services.AddScoped<IRepositoryPaymentManager, RepositoryPaymentManager>();
            services.AddScoped<IRepositoryManagerUser, RepositoryManagerUser>();
            services.AddScoped<IRepositorySOManager, RepositorySOManager>();
        }
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IServiceManagerMaster, ServiceManagerMaster>();
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();
            services.AddScoped<IServiceSOManager, ServiceSOManager>();
            services.AddScoped<IServiceRequestSOManager, ServiceRequestSOManager>();
            services.AddScoped<IServicePaymentManager, ServicePaymentManager>();
        }

    }
}
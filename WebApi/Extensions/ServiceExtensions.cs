using Domain.Repositories.Partners;
using Domain.Repositories.Master;
using Domain.Repositories.Payment;
using Domain.Authentication;
using Domain.Repositories.CR;
using Domain.Repositories.UserModule;
using Mapster;
using Domain.Repositories.SO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories;
using Persistence.Repositories.Partners;
using Persistence.Repositories.UserModule;
using Service.Abstraction.Partners;
using Persistence.Repositories.Master;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Service.Abstraction.User;
using Service.Partners;
using System.Reflection;
using Service.Master;
using Service.SO;
using Service.UserModule;
using System.Text;
using Persistence.Repositories.SO;
using Persistence.Repositories.CR;
using Service.HR;
using Service.Abstraction.HR;
using Domain.Repositories.HR;
using Persistence.Repositories.HR;

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
            services.AddScoped<IRepositoryCustomerManager, RepositoryCustomerManager>();
            services.AddScoped<IRepositoryPartnerManager, RepositoryPartnerManager>();
            services.AddScoped<IRepositoryHRManager, RepositoryHRManager>();
        }
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IServiceManagerMaster, ServiceManagerMaster>();
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();
            services.AddScoped<IServiceSOManager, ServiceSOManager>();
            services.AddScoped<IServiceRequestSOManager, ServiceRequestSOManager>();
            services.AddScoped<IServicePartnerManager, ServicePartnerManager>();
            services.AddScoped<IServiceHRManager, ServiceHRManager>();
        }
        public static void ConfigureMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            
        }

        public static void ConfigureJwtGenerator(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddScoped<JwtTokenGenerator>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }
    }
}
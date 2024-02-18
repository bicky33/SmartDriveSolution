using Domain.Repositories.Master;
using Domain.Repositories.Payment;
using Domain.Authentication;
using Domain.Repositories.CR;
using Domain.Repositories.UserModule;
using Domain.Repositories.SO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using Persistence.SO;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories;
using Persistence.Repositories.CR;
using Service.Abstraction.CR;
using Service.CR;
using Persistence.Repositories.UserModule;
using Persistence.Repositories.Master;
using Persistence.Repositories.SO;
using Service.Abstraction.Master;
using Service.Abstraction.SO;
using Service.Abstraction.Payment;
using Service.Base;
using Persistence.Repositories.UserModule;
using Service.Abstraction.User;
using Service.Master;
using Service.SO;
using Service.UserModule;
using System.Text;

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
        }
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IServiceManagerMaster, ServiceManagerMaster>();
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();
            services.AddScoped<IServiceSOManager, ServiceSOManager>();
            services.AddScoped<IServiceRequestSOManager, ServiceRequestSOManager>();
            services.AddScoped<IServicePaymentManager, ServicePaymentManager>();
            services.AddScoped<IServiceCustomerManager, ServiceCustomerManager>();
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
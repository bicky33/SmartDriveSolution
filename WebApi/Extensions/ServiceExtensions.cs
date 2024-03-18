using Domain.Authentication;
using Domain.Repositories.CR;
using Domain.Repositories.HR;
using Domain.Repositories.Master;
using Domain.Repositories.Partners;
using Domain.Repositories.Payment;
using Domain.Repositories.SO;
using Domain.Repositories.UserModule;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Base;
using Persistence.Repositories;
using Persistence.Repositories.CR;
using Persistence.Repositories.HR;
using Persistence.Repositories.Master;
using Persistence.Repositories.Partners;
using Persistence.Repositories.SO;
using Persistence.Repositories.UserModule;
using Quartz;
using Service.Abstraction.HR;
using Service.Abstraction.Master;
using Service.Abstraction.Partners;
using Service.Abstraction.Payment;
using Service.Abstraction.SO;
using Service.Abstraction.User;
using Service.Base;
using Service.HR;
using Service.Master;
using Service.Partners;
using Service.SO;
using Service.SO.BackgroundService;
using Service.UserModule;
using System.Reflection;
using System.Text;
using Persistence.Repositories.SO;
using Persistence.Repositories.CR;
using Service.Abstraction.Payment;
using Service.Base;
using Service.HR;
using Domain.Repositories.HR;
using Persistence.Repositories.HR;
using Service.Abstraction.HR;
using Service.Abstraction.CR;
using Service.CR;
using Service.Abstraction.Payment;
using Service.Base;
using Persistence.Repositories.Payment;

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
                   .AllowAnyHeader()
                   .WithExposedHeaders("X-Total-Pages", "X-Current-Pages", "X-HasNext", "X-HasPrevious", "X-Total-Count"));
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
            services.AddScoped<IServicePaymentManager, ServicePaymentManager>();
            services.AddScoped<IServiceManagerUser, ServiceManagerUser>();
            services.AddScoped<IServiceSOManager, ServiceSOManager>();
            services.AddScoped<IServicePartnerManager, ServicePartnerManager>();
            services.AddScoped<IServiceCustomerManager, ServiceCustomerManager>();
            services.AddScoped<IServiceHRManager, ServiceHRManager>();
            services.AddScoped<IMailService, MailService>();
        }
        public static void ConfigureMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

        }

        public static void ConfigureJwtGenerator(this IServiceCollection services, IConfiguration configuration)
        {
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
        public static void ConfigureSchedulingJobs(this IServiceCollection services, IConfiguration configuration)
        {
            // base configuration from appsettings.json
            //services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));

            //// if you are using persistent job store, you might want to alter some options
            //services.Configure<QuartzOptions>(options =>
            //{
            //    options.Scheduling.IgnoreDuplicates = true; // default: false
            //    options.Scheduling.OverWriteExistingData = true; // default: true
            //});
            services.AddQuartz();
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
            services.ConfigureOptions<BackgroundServiceSOJobSetup>();
        }
    }
}
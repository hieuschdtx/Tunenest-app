using System.Data;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Scrutor;
using tunenest.Api.Configurations;
using tunenest.Api.Middlewares;
using tunenest.Application.Behaviors;
using tunenest.Domain.Consts;
using tunenest.Infrastructure.Configurations;
using tunenest.Infrastructure.Options;
using tunenest.Persistence.Data;

namespace tunenest.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssemblies(
                Infrastructure.AssemblyReference.assembly,
                Domain.AssemblyReference.assembly,
                Application.AssemblyReference.assembly,
                Persistence.AssemblyReference.assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            //MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.assembly));

            //PipelineBehavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));

            //FluentValidation
            services.AddValidatorsFromAssembly(Application.AssemblyReference.assembly, includeInternalTypes: true);

            //DbContext
            services.AddDbContext<TunenestDbContext>(option => { option.UseNpgsql(connectionString); });
            services.AddTransient(provider => provider.GetRequiredService<IDbConnection>().BeginTransaction());
            services.AddTransient<IDbConnection>(_ =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });

            //AutoMapper
            services.AddAutoMapper(Application.AssemblyReference.assembly);

            //Option Service
            services.Configure<AuthenticationOption>(configuration.GetSection("Jwt"));
            services.Configure<CloudinaryOption>(configuration.GetSection("Cloudinary"));

            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, AppSetting appSetting)
        {
            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = appSetting.jwtBearerSetting.Name;
                options.DefaultChallengeScheme = appSetting.jwtBearerSetting.Name;
            })
                .AddJwtBearer()
                .AddCookie(appSetting.cookieSettings.Name, options =>
                {
                    options.SlidingExpiration = true;
                    options.Cookie.Name = appSetting.cookieSettings.Name;
                    options.Cookie.Domain = appSetting.cookieSettings.Domain;
                    options.Cookie.HttpOnly = appSetting.cookieSettings.HttpOnly;
                    options.Cookie.SameSite = SameSiteMode.None;
                    //     appSetting.cookieSettings.SameSite == "Lax" ? SameSiteMode.Lax : SameSiteMode.None;
                    options.Cookie.SecurePolicy = appSetting.cookieSettings.SecurePolicy
                        ? CookieSecurePolicy.Always
                        : CookieSecurePolicy.None;
                });

            //Authorization
            services.AddAuthorization(options =>
            {
                var builder = new AuthorizationPolicyBuilder(appSetting.jwtBearerSetting.Name, appSetting.cookieSettings.Name);

                builder = builder.RequireAuthenticatedUser();
                options.DefaultPolicy = builder.Build();

                options.AddPolicy(RoleConst.Guest, policy =>
                {
                    policy.RequireRole(RoleConst.Administrator, RoleConst.Employee, RoleConst.Guest)
                        .AddAuthenticationSchemes(appSetting.cookieSettings.Name);
                });

                options.AddPolicy(RoleConst.Employee, policy =>
                {
                    policy.RequireRole(RoleConst.Administrator, RoleConst.Employee)
                        .AddAuthenticationSchemes(appSetting.cookieSettings.Name);
                });

                options.AddPolicy(RoleConst.Administrator, policy =>
                {
                    policy.RequireRole(RoleConst.Administrator)
                        .AddAuthenticationSchemes(appSetting.cookieSettings.Name);
                });
            });

            services.ConfigureOptions<AuthenticationOptionConfiguration>();
            services.ConfigureOptions<JwtBearerConfiguration>();
            services.AddSingleton<AuthenticationSetting>();

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AppAuthorizationMiddlewareResultHandler>();

            return services;
        }
    }
}

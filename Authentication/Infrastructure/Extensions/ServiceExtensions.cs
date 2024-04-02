using Authentication.Data.Context;
using Authentication.Models.Configurations;
using Authentication.Models.Entities.Roles;
using Authentication.Models.Entities.Users;
using Authentication.Repositories.Authentications;
using Authentication.Repositories.Roles;
using Authentication.Repositories.Users;
using Authentication.Services.Foundations.Authentications;
using Authentication.Services.Foundations.Roles;
using Authentication.Services.Foundations.Users;
using Authentication.Services.Orchestrations.Users;
using Authentication.Services.Processings.Authentications;
using Authentication.Services.Processings.Roles;
using Authentication.Services.Processings.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Authentication.Infrastructure.Extensions
{
    public static partial class ServiceExtensions
    {
        public static void AddCustomAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AuthConfiguration authConfiguration = configuration
                .GetSection(nameof(AuthConfiguration))
                .Get<AuthConfiguration>();

            PasswordConfiguration passwordConfiguration = configuration
                .GetSection(nameof(PasswordConfiguration))
                .Get<PasswordConfiguration>();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = passwordConfiguration.RequiredLength;
                options.Password.RequireDigit = passwordConfiguration.RequireDigit;
                options.Password.RequireLowercase = passwordConfiguration.RequireLowercase;
                options.Password.RequireUppercase = passwordConfiguration.RequireUppercase;
                options.Password.RequireNonAlphanumeric = passwordConfiguration.RequireNonAlphanumeric;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<AuthenticationDbContext>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = authConfiguration.ValidateIssuer,
                        ValidateAudience = authConfiguration.ValidateAudience,
                        ValidateIssuerSigningKey = authConfiguration.ValidateIssuerSigningKey,
                        RequireExpirationTime = authConfiguration.RequireExpirationTime,
                        ValidateLifetime = authConfiguration.ValidateLifetime,
                        RequireSignedTokens = authConfiguration.RequireSignedTokens,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(authConfiguration.SigningKey)),
                    };
                });
        }

        public static void AddAuthenticationContext(this IServiceCollection services)
        {
            services.AddDbContext<AuthenticationDbContext>();
        }

        public static void AddAuthenticationRepositories(this IServiceCollection services)
        {
            //services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            //services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }

        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IAuthenticationProcessingService, AuthenticationProcessingService>();
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            services.AddTransient<IRoleProcessingService, RoleProcessingService>();

            services.AddTransient<IUserOrchestrationService, UserOrchestrationService>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "WorkHub.Api",
                    Version = "v1",
                };

                options.SwaggerDoc(
                    name: "v1",
                    info: openApiInfo);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }


        //public static void AddCustomHealthChecks(
        //    this IHealthChecksBuilder healthChecksBuilder,
        //    IConfiguration configuration)
        //{
        //    string connectionString = configuration.GetConnectionString(
        //        name: "DefaultConnection");

        //    healthChecksBuilder
        //        .AddDbContextCheck<AuthenticationDbContext>(nameof(AuthenticationDbContext))
        //        .AddSqlServer(
        //            connectionString,
        //            name: "PostgreSQL");
        //}
    }
}

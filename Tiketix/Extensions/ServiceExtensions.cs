using System.Text;
using AspNetCoreRateLimit;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service;
using Service.Contracts;

namespace tiketix.Extensions
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
        // public static void ConfigureCors(this IServiceCollection services) =>
        //     services.AddCors(options =>
        //     {
        //         options.AddPolicy("AllowLocalhost3000", builder =>
        //                                         builder.WithOrigins("http://localhost:3000")
        //                                         .AllowAnyMethod()
        //                                         .AllowAnyHeader()
        //                                         .AllowCredentials());
        //     });


        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                                services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
                                services.AddScoped<IServiceManager, ServiceManager>();

        // public static void ConfigureSqlContext(this IServiceCollection services) =>
        //                         services.AddDbContext<RepositoryContext>(opts =>
        //                         opts.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection")));

        public static void ConfigureSqlContext(this IServiceCollection services) =>
                                services.AddDbContext<RepositoryContext>(opts =>
                                opts.UseMySql(Environment.GetEnvironmentVariable("DefaultConnection2"),
                                ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DefaultConnection2"))));

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
            {
                new() {
                Endpoint = "*",
                Limit = 30,
                Period = "5m"
                }
            };
            services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules = 
            rateLimitRules; });
            services.AddSingleton<IRateLimitCounterStore, 
            MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();


        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
                
                #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string secretKey = Environment.GetEnvironmentVariable("Jwt_Key");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                #pragma warning disable CS8604 // Possible null reference argument.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagerWebApi.Models.Services;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi
{
    public static class Extensions
    {
        public static IServiceCollection AddData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITaskService, TaskService>();
            serviceCollection.AddScoped<IProjectService, ProjectService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IJWTService, JWTService>();
            serviceCollection.AddScoped<IMemberService, MemberService>();
            serviceCollection.AddScoped<IAdminService, AdminService>();
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
            return serviceCollection;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var configSection = configuration.GetSection(nameof(AuthSettings));

            serviceCollection.Configure<AuthSettings>(configSection);

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configSection.Get<AuthSettings>().SecretKey))
                    };
                });

            return serviceCollection;
        }
    }
}

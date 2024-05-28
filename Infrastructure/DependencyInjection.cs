using Application.Common.Interfaces;
using Domain.Identity;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationEFContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionWrite")));

            services.AddScoped<IApplicationEFContext>(provider => provider.GetService<ApplicationEFContext>());

            services.AddIdentity<User, Role>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationEFContext>()
            .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
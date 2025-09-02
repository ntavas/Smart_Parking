using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartParking.Application.Services.Interfaces;
using SmartParking.Infrastructure.Persistence;
using SmartParking.Infrastructure.Repositories;
using SmartParking.Infrastructure.Security;

namespace SmartParking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Postgres")
                 ?? throw new InvalidOperationException("Missing connection string 'Postgres'.");

        services.AddDbContext<SmpDbContext>(opt =>
            opt.UseNpgsql(cs, npg => npg.EnableRetryOnFailure())
                .UseSnakeCaseNamingConvention());
        
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IUserRepository, UserRepository>();

        
        return services;
    }
}
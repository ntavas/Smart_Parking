using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartParking.Application.Services;
using SmartParking.Application.Services.Interfaces;

namespace SmartParking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
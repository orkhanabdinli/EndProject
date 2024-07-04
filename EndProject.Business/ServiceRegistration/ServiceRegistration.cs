using EndProject.Business.Services.Implementations;
using EndProject.Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EndProject.Business.ServiceRegistration;

public static class ServiceRegistiration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileMediaService, UserProfileMediaService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();
        services.AddScoped<IPostService, PostService>();
    }
}

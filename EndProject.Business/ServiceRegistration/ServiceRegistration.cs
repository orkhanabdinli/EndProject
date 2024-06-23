using EndProject.Business.Services.Implementations;
using EndProject.Business.Services.Interfaces;
using EndProject.Core.Repositories;
using EndProject.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Business.ServiceRegistration;

public static class ServiceRegistiration
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserProfileMediaService, UserProfileMediaService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();
    }
}

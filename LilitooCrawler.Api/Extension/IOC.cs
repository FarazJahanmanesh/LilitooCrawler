using Application.Services;
using Domain.Interfaces.ExternalServices;
using Domain.Interfaces.Services;
using ExternalServices.Db;
using ExternalServices.Services;

namespace LilitooCrawler.Api.Extension;
public static class IOC
{
    public static IServiceCollection RegisterIOC(this IServiceCollection services)
    {
        services.RegisterExternalServices()
            .RegisterServices();
        return services;
    }
    private static IServiceCollection RegisterExternalServices(this IServiceCollection services)
    {
        services.AddScoped<ILilitooServices, LilitooServices>();
        return services;
    }
    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ILilitooReadServices, LilitooReadServices>();
        return services;
    
}

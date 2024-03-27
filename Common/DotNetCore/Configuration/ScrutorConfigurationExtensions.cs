using Common.DotNetCore.Utilities.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.DotNetCore.Configuration;

public static class ScrutorConfigurationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(selector => selector
        .FromAssemblies(assemblies)
        .AddClasses(c => { c.AssignableTo<IScopedDependency>(); })
        .AsImplementedInterfaces()
        .WithScopedLifetime()
        .AddClasses(c => { c.AssignableTo<ITransientDependency>(); })
        .AsImplementedInterfaces()
        .WithTransientLifetime()
        .AddClasses(c => { c.AssignableTo<ISingeltonDependency>(); })
        .AsImplementedInterfaces()
        .WithSingletonLifetime()
        );

        return services;
    }

}

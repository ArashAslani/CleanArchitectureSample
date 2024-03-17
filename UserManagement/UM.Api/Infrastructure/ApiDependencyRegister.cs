using UM.Api.Infrastructure.Jwt;

namespace UM.Api.Infrastructure;

public static class ApiDependencyRegister
{
    public static void RegisterApiDependecy(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddJwtAuthentication(configuration);
        service.AddTransient<CustomJwtValidation>();
    }
}
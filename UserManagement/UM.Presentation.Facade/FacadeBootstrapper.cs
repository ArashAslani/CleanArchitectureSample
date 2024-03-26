using Microsoft.Extensions.DependencyInjection;
using UM.ServiceHost.Facade.Roles;
using UM.ServiceHost.Facade.Users;

namespace UM.ServiceHost.Facade
{
    public static class FacadeBootstrapper
    {
        public static IServiceCollection InitFacadeDependency(this IServiceCollection services)
        {
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<IUserFacade, UserFacade>();
            return services;
        }
    }
}

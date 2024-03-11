using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UM.Application.UserApplication;
using UM.Application.UserApplication.Register;
using UM.Application.Utilities;
using UM.Domain.UserAgg.Services;
using UM.Infrastructure;

namespace UM.Bootstrapper
{
    public static class ApplicationBootstrapper
    {
        public static void RegisterShopDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(ctf => ctf.RegisterServicesFromAssembly(typeof(Directories).Assembly));
            //services.AddMediatR(ctf => ctf.RegisterServicesFromAssembly(typeof(GetCategoryByIdQuery).Assembly));
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddValidatorsFromAssembly(typeof(RegisterUserCommand).Assembly);
        }
    }
}

using FluentValidation;
using MediatR;
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
        public static IServiceCollection RegisterUserManagementDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(typeof(Directories).Assembly);
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddValidatorsFromAssembly(typeof(RegisterUserCommand).Assembly);

            return services;
        }
    }
}

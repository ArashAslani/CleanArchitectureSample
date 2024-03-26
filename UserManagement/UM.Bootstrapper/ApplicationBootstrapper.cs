using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UM.Application.Users;
using UM.Application.Users.Create;
using UM.Application.Utilities;
using UM.Domain.Users.Services;
using UM.Infrastructure;
using UM.Query.Users.GetByPhoneNumber;
using UM.ServiceHost.Facade;

namespace UM.Bootstrapper
{
    public static class ApplicationBootstrapper
    {
        public static IServiceCollection RegisterUserManagementDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(typeof(Directories).Assembly, typeof(GetUserByPhoneNumberQuery).GetTypeInfo().Assembly);
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);
            services.InitFacadeDependency();

            return services;
        }
    }
}

﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UM.Application.Users.Create;
using UM.Application.Utilities;
using UM.Infrastructure;
using UM.Query.Users.GetByPhoneNumber;

namespace UM.Bootstrapper;

public static class ApplicationBootstrapper
{
    public static IServiceCollection AddUserManagementDependency(this IServiceCollection services, string connectionString)
    {
        InfrastructureBootstrapper.Init(services, connectionString);
        services.AddMediatR(typeof(Directories).Assembly, typeof(GetUserByPhoneNumberQuery).GetTypeInfo().Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateUserCommand).Assembly);

        return services;
    }
}

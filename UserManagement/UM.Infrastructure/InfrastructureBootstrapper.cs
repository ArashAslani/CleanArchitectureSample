﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UM.Domain.RoleAgg.Repository;
using UM.Domain.UserAgg.Repository;
using UM.Infrastructure.Persistant.Dapper;
using UM.Infrastructure.Persistent.EFCore;
using UM.Infrastructure.Persistent.EFCore.RoleAgg;
using UM.Infrastructure.Persistent.EFCore.UserAgg;
using UM.Infrastructure.Utilities.MediatR;

namespace UM.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
       
        services.AddTransient<IRoleRepository, RoleRepository>();
       
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddSingleton<ICustomPublisher, CustomPublisher>();

        services.AddTransient(_ => new DapperContext(connectionString));

        services.AddDbContext<UserManagementContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}

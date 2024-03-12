using Microsoft.Extensions.DependencyInjection;
using UM.Domain.RoleAgg.Repository;
using UM.Domain.UserAgg.Repository;
using UM.Infrastructure.Persistent.EFCore;
using UM.Infrastructure.Utilities.MediatR;

namespace UM.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
       
        services.AddTransient<IRoleRepository, RoleRepository>();
       
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddSingleton<ICustomPublisher, CustomPublisher>();

        services.AddDbContext<UserManagmentContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}

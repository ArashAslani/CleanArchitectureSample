using Microsoft.Extensions.DependencyInjection;

namespace UM.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void Init(IServiceCollection services, string connectionString)
    {
       
        services.AddTransient<IRoleRepository, RoleRepository>();
       
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddSingleton<ICustomPublisher, CustomPublisher>();

        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}

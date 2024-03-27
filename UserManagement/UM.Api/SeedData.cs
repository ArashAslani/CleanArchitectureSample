using Common.Application.SecurityUtilities;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Reflection;
using UM.Application.Users;
using UM.Application.Utilities;
using UM.Domain.RoleAgg;
using UM.Domain.RoleAgg.Enums;
using UM.Domain.Users;
using UM.Domain.Users.Repository;
using UM.Domain.Users.Services;
using UM.Infrastructure.Persistent.EFCore.Users;
using UM.Infrastructure.Utilities.MediatR;
using UM.Query.Users.GetByPhoneNumber;

namespace UM.Infrastructure.Persistent.EFCore;

public class SeedData
{
    public static void EnsureSeedData(string connectionString)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<UserManagementContext>(
            options => options.UseSqlServer(connectionString)
        );
        services.AddTransient<ICustomPublisher, CustomPublisher>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddMediatR(typeof(Directories).Assembly, typeof(GetUserByPhoneNumberQuery).GetTypeInfo().Assembly);


        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
        
        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(SeedData).Name);

            var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                        onRetry: (exception, retryCount, context) =>
                        {
                            string message = $"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.";
                            logger.LogError(message: message);
                        });

            var ctx = scope.ServiceProvider.GetRequiredService<UserManagementContext>();
            //if the sql server container is not created on run docker compose this
            //migration can't fail for network related exception. The retry options for DbContext only 
            //apply to transient exceptions                    

            ctx.Database.Migrate();

            EnsureUsers(scope);

            logger.LogInformation("Migrated database associated with context {DbContextName}", nameof(SeedData));
        }
        catch (SqlException ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(SeedData));
        }
    }

    private static void EnsureUsers(IServiceScope scope)
    {
        var userDomainService = scope.ServiceProvider.GetService<IUserDomainService>();
        var applicationDbContext = scope.ServiceProvider.GetRequiredService<UserManagementContext>();

        if (!applicationDbContext.Roles.Any(x => x.Title.Equals("Admin")) || !applicationDbContext.Users.Any(x => x.PhoneNumber.Equals("09139015261")))
        {
            var adminRole = Role.CreateNew("Admin");
            var rolePermissions = new List<RolePermission>()
            {
                new(Permission.PanelAdmin),
                new(Permission.EditProfile),
                new(Permission.Role_Management),
                new(Permission.User_Management),
                new(Permission.CURD_User),
                new(Permission.ChangePassword)
            };
            adminRole.SetPermissions(rolePermissions);
            applicationDbContext.Roles.Add(adminRole);

            var password = Sha256Hasher.Hash("ABCd1234");
            var user = User.CreateNew("Arash", "Aslani", "09139015261", "ArashAslani@Gmail.com", password, Domain.Users.Enums.Gender.Male, userDomainService);
            var userRoleList = new List<UserRole>() { new(adminRole.Id) };
            user.SetRoles(userRoleList);
            applicationDbContext.Users.Add(user);

            applicationDbContext.SaveChanges();
            
        }
    }
}
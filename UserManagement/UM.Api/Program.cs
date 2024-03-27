using Common.Application;
using UM.Bootstrapper;
using Common.DotNetCore.Middlewares;
using UM.Api.Infrastructure;
using UM.Infrastructure.Persistent.EFCore;
using Common.DotNetCore.Swagger;
using System.Reflection;
using UM.ServiceHost.Facade;
using UM.Application;
using UM.Domain;
using UM.Infrastructure;
using UM.Query;
using Common;
using Common.DotNetCore.Configuration;


namespace UM.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMinimalMvc();

        var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

        //Seeding Data
        SeedData.EnsureSeedData(connectionString: connectionString);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddServices(Assembly.GetExecutingAssembly(),
            typeof(ApiAssembly).Assembly,
            typeof(ApplicationAssembly).Assembly,
            typeof(DomainAssembly).Assembly,
            typeof(InfrastructureAssembly).Assembly,
            typeof(FacadeAssembly).Assembly,
            typeof(QueryAssembly).Assembly,
            typeof(CommonAssembly).Assembly);


        // Add services to the container.
        builder.Services.AddUserManagementDependency(connectionString);
        builder.Services.AddCommonServiceCollections();
        builder.Services.AddJwtConfigs(builder.Configuration);
        builder.Services.AddCorsConfigs();

        //builder.Services.AddTransient<IFileService, FileService>();

        //Add Distributed Cache
        builder.Services.AddDistributedMemoryCache(); // By defult use in memory

        //Api Versioning Configuration
        builder.Services.AddApiVersioning();
        builder.Services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        //Swagger Configuration
        builder.Services.AddSwagger();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerAndUi();
        }
        app.UseApiCustomExceptionHandler();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCors("UM.Api");

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

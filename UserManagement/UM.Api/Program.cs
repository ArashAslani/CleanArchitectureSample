
using Common.Application;
using Common.Application.FileUtilities.Contracts;
using Common.Application.FileUtilities.Services;
using UM.Bootstrapper;
using Common.DotNetCore.Middlewares;
using UM.Api.Infrastructure.Jwt;
using UM.Api.Infrastructure;
using UM.Infrastructure.Persistent.EFCore;
using Common.DotNetCore.Swagger;


namespace UM.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCustomControllerConfig();
        builder.Services.AddSwaggerGenNewtonsoftSupport();

        var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

        //Seeding Data
        SeedData.EnsureSeedData(connectionString: connectionString);

        // Add services to the container.
        builder.Services.RegisterUserManagementDependency(connectionString);
        builder.Services.RegisterCommonDependency();
        builder.Services.RegisterApiDependecy(builder.Configuration);
        builder.Services.AddTransient<IFileService, FileService>();
        builder.Services.AddTransient<CustomJwtValidation>();


        //Api Versioning Configuration
        builder.Services.AddApiVersioning();

        builder.Services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

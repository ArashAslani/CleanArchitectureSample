
using Common.Application;
using Common.Application.FileUtilities.Contracts;
using Common.Application.FileUtilities.Services;
using UM.Bootstrapper;
using Common.DotNetCore.Middlewares;
using UM.Api.Infrastructure.Jwt;
using UM.Api.Infrastructure;
using UM.Infrastructure.Persistent.EFCore;

namespace UM.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");
        builder.Services.RegisterUserManagementDependency(connectionString);
        builder.Services.RegisterCommonDependency();
        builder.Services.RegisterApiDependecy(builder.Configuration);
        builder.Services.AddTransient<IFileService, FileService>();
        builder.Services.AddTransient<CustomJwtValidation>();

        //Seeding Data
        SeedData.EnsureSeedData(connectionString: connectionString); //Has Bug

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseApiCustomExceptionHandler();

        app.MapControllers();

        app.Run();
    }
}

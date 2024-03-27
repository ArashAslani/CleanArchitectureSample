using Common.DotNetCore;
using Common.DotNetCore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UM.Api.Infrastructure.Jwt;

namespace UM.Api.Infrastructure;

public static class ApiDependencyRegister
{
    public static void RegisterApiDependecy(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddJwtAuthentication(configuration);
        service.AddTransient<CustomJwtValidation>();

        service.AddCors(options =>
        {
            options.AddPolicy(name: "UM.Api",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }

    public static void AddCustomControllerConfig(this IServiceCollection service)
    {
        service.AddControllers()
            .ConfigureApiBehaviorOptions(option =>
            {
                option.InvalidModelStateResponseFactory = (context =>
                {
                    var result = new ApiResult()
                    {
                        IsSuccess = false,
                        MetaData = new()
                        {
                            AppStatusCode = AppStatusCode.BadRequest,
                            Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
                        }
                    };
                    return new BadRequestObjectResult(result);
                });
            }).AddNewtonsoftJson(option => // enum as a string | swagger config
            {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            });
    }
}
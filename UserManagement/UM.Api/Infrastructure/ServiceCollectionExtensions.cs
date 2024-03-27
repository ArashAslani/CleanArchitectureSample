using Common.DotNetCore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Common.DotNetCore;
using UM.Api.Infrastructure;
using UM.Api.Infrastructure.Jwt;

namespace UM.Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddJwtConfigs(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddJwtAuthentication(configuration);
    }

    public static void AddCorsConfigs(this IServiceCollection service)
    {
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

    public static void AddMinimalMvc(this IServiceCollection services)
    {
        services.AddControllers()
        .ConfigureApiBehaviorOptions(option =>
        {
            option.InvalidModelStateResponseFactory = context =>
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
            };
        }).AddNewtonsoftJson(option => // enum as a string | swagger config
        {
            option.SerializerSettings.Converters.Add(new StringEnumConverter());
            option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        });
    }
}

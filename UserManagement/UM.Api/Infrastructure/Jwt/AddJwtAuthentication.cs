using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace UM.Api.Infrastructure.Jwt;

public static class JwtAuthenticationConfig
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"])),
                ValidateLifetime = true,

                ValidateIssuer = true,
                ValidIssuer = configuration["JwtConfig:Issuer"],

                ValidateAudience = true,
                ValidAudience = configuration["JwtConfig:Audience"],


                ValidateIssuerSigningKey = true,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:EncryptKey"]))
            };
            option.SaveToken = true;
            option.Events = new JwtBearerEvents()
            {
                OnTokenValidated = async context =>
                {
                    var customValidate = context.HttpContext.RequestServices
                        .GetRequiredService<CustomJwtValidation>();
                    await customValidate.Validate(context);
                }
            };
        });
    }
}
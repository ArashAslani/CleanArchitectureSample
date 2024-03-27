using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UM.Query.Users.DTOs;

namespace UM.Api.Infrastructure.Jwt;

public class JwtTokenBuilder
{
    public static JwtSecurityToken BuildToken(UserDto user, IConfiguration configuration)
    {
        var roles = user.Roles.Select(s => s.RoleTitle);
        var claims = new List<Claim>()
        {
            new(ClaimTypes.MobilePhone,user.PhoneNumber),
            new(ClaimTypes.NameIdentifier,user.Id.Value.ToString()),
            new(ClaimTypes.Role,string.Join("-",roles))
        };
        var secretKey = Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"]);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        var encryptionKey = Encoding.UTF8.GetBytes(configuration["JwtConfig:EncryptKey"]);
        var encryptionCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW,
            SecurityAlgorithms.Aes128CbcHmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["JwtConfig:Issuer"],
            Audience = configuration["JwtConfig:Audience"],
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(double.Parse(configuration["JwtConfig:NotBeforeMinutes"])),
            Expires = DateTime.Now.AddMinutes(double.Parse(configuration["JwtConfig:ExpirationMinutes"])),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptionCredentials,
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
        
        return securityToken;
    }

    
}